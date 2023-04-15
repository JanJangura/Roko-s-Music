using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
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
                }
                else if(Mathf.Abs(transform.position.y) > 0.12f)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, hitEffect.transform.rotation);
                }
                else 
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, hitEffect.transform.rotation);
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
                Instantiate(missEffect, transform.position, hitEffect.transform.rotation);
            }

        }
    }
}
