using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource theMusic;
    public bool startPlaying;
    public float beatTempo = 93;
    public bool DROPTHEBEAT;

    [Header("Header Stuff")]
    public static GameManager instance;
    public GameObject[] ToggleGameObjectsOnOff;

    [Header("Score Values")]
    public int currentScore;
    public float GameTime = 200;
    public float shutOffTime = 5;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    [Header("Game Mode")]
    public float Easy;
    public float Medium;
    public float Hard;
    

    [Header("ScoreMultipliers")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    [Header("UGI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public int counter = 0;
    public int STOPINVOKE = 0;
    public TextMeshProUGUI Timer;
    float timer = 0;
    public GameObject pressAnything;
    HealthBar healthBar;

    [Header("UGI Results")]
    public GameObject Results;
    public TextMeshProUGUI NormalNotesHit;
    public TextMeshProUGUI GoodNotesHit;
    public TextMeshProUGUI PerfectNotesHit;
    public TextMeshProUGUI MissedNotes;
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI PercentageHit;
    public bool endGame;
    public int normalNotesHit = 0;
    public int goodNotesHit = 0;
    public int perfectNotesHit = 0;
    public int missedNotes = 0;
    public int finalScore;
    float percentageHit;

    [Header("HEALTH")]
    public int currentHealth = 50;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;     
        DROPTHEBEAT = false;
        Easy = beatTempo / 60;
        Medium = beatTempo / 60 * .25f * 2;
        Hard = beatTempo / 60 * .25f * 1;

        currentMultiplier = 1;
        endGame = false;

        scoreText.text = "Score: " + 0;
        multiText.text = "Multiplier: x" + currentMultiplier;      
        pressAnything.SetActive(true);
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        maxHealth = currentHealth;

        healthBar.SetMaxHealth(maxHealth);

        for (int i = 0; i < ToggleGameObjectsOnOff.Length; i++)
        {
            ToggleGameObjectsOnOff[i].SetActive(false);
        }

        Results.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                DROPTHEBEAT = true;
                startPlaying = true;

                for (int i = 0; i < ToggleGameObjectsOnOff.Length; i++)
                {
                    ToggleGameObjectsOnOff[i].SetActive(true);
                }
                timer = GameTime;
                theMusic.Play();
                pressAnything.SetActive(false);
                Invoke("TurnOffRandomSpawner", shutOffTime);
            }
        }

        if (DROPTHEBEAT && STOPINVOKE == 0)
        {
            Invoke("ENDGAME", GameTime);
            STOPINVOKE = 1;
        }

        timer -= 1 * UnityEngine.Time.deltaTime;
        Timer.text = "Timer: " + timer.ToString("0");

        if (endGame)
        {
            Invoke("ENDGAME", 5);
        }
    }

    public void TurnOffRandomSpawner()
    {
        endGame = true;
    }

    public void NoteHit()
    {

        if(currentMultiplier < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
                counter++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        counter = 0;
    }

    public void ENDGAME()
    {
        Display();
        DROPTHEBEAT = false;       
        endGame = true;
        Results.SetActive(true);
        theMusic.Pause();
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void DamageTaken(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            endGame = true;
        }
    }

    public void NormalNotes(int i)
    {
        normalNotesHit += i;
    }
    public void GoodNotes(int i)
    {
        goodNotesHit += i;
    }
    public void PerfectNotes(int i)
    {
        perfectNotesHit += i;
    }

    public void Missed(int i)
    {
        missedNotes += i;
    }

    public void Display()
    {
        finalScore = currentScore;
        int totalHits = (normalNotesHit + goodNotesHit + perfectNotesHit);

        if (missedNotes > 0){
            percentageHit = totalHits / (missedNotes + totalHits);
        }
        else
        {
            percentageHit = 100;
        }

        for (int i = 0; i < ToggleGameObjectsOnOff.Length; i++)
        {
            ToggleGameObjectsOnOff[i].SetActive(false);
        }

        NormalNotesHit.text = "Normal Notes Hit: " + normalNotesHit + " Hits!";
        GoodNotesHit.text = "Good Notes Hit: " + goodNotesHit + " Hits!";
        PerfectNotesHit.text = "Perfect Notes Hit: " + perfectNotesHit + " Hits!";
        MissedNotes.text = "Missed Notes Hit: " + missedNotes + " Missed!";
        PercentageHit.text = "Percentage Hit: " + percentageHit + " %";
        FinalScore.text = "FINAL SCORE: " + currentScore + " PTS"; ;
    }
}
