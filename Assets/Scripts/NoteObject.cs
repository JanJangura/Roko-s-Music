using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                this.gameObject.tag = "Null";
                Destroy(this.gameObject);
                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) > 0.44f)
                {
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    gameManager.NormalNotes(1);
                }
                else if(Mathf.Abs(transform.position.y) > 0.12f)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, hitEffect.transform.rotation);
                    gameManager.GoodNotes(1);
                }
                else 
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, hitEffect.transform.rotation);
                    gameManager.PerfectNotes(1);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
        if (other.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }           
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            if(this.tag != "Null")
            {
                canBePressed = false;
                GameManager.instance.NoteMissed();
                Destroy(this.gameObject);
                Instantiate(missEffect, transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), hitEffect.transform.rotation);
                gameManager.DamageTaken(1);
                gameManager.Missed(1);
            }

        }
    }
}
