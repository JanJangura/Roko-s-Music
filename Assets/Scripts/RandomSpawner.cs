using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spawnPrefab1;
    public GameObject spawnPrefab2;
    public GameObject spawnPrefab3;
    public GameObject spawnPrefab4;
    public GameManager GM;

    private int randSpawner;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeatSpawner", 0f);
    }

    // Update is called once per frame
    void Update()
    {        
        if (GM.DROPTHEBEAT)
        {
            randSpawner = Random.Range(1, 5);
        }        
    }

    public void BeatSpawner()
    {
        if (!GM.endGame)
        {
            switch (randSpawner)
            {
                case 1:
                    Instantiate(spawnPrefab1, spawnPoints[0].position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(spawnPrefab2, spawnPoints[1].position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(spawnPrefab3, spawnPoints[2].position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(spawnPrefab4, spawnPoints[3].position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            switch (GM.counter)
            {
                case 1:
                    Invoke("BeatSpawner", GM.Medium);
                    break;
                case 2:
                    Invoke("BeatSpawner", GM.Hard);
                    break;
                default:
                    Invoke("BeatSpawner", GM.Easy);
                    break;
            }
        }       
    }
}
