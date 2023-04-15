using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public static GameManager instance;
    public GameObject Dancer;

    public int currentScore;
    public int GameTime = 200;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public float beatTempo = 93;
    public bool DROPTHEBEAT;

    public float Easy;
    public float Medium;
    public float Hard;
    public bool endGame;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public int counter = 0;
    public int STOPINVOKE = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Dancer.SetActive(false);
        DROPTHEBEAT = false;
        Easy = beatTempo / 60;
        Medium = beatTempo / 60 * .25f * 2;
        Hard = beatTempo / 60 * .25f * 1;

        currentMultiplier = 1;
        endGame = false;

        scoreText.text = "Score: " + 0;
        multiText.text = "Multiplier: x" + currentMultiplier;        
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
                Dancer.SetActive(true);
                theMusic.Play();
            }
        }

        if (DROPTHEBEAT && STOPINVOKE == 0)
        {
            Invoke("ENDGAME", GameTime);
            STOPINVOKE = 1;
        }
    }

    public void NoteHit()
    {
        Debug.Log("HitOnTime");

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
        Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        counter = 0;
    }

    public void ENDGAME()
    {
        Debug.Log("EndGAME");
        DROPTHEBEAT = false;       
        endGame = true;
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
}
