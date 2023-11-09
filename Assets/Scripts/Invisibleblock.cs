using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibleblock : MonoBehaviour
{
    //private bool aboutToBeHit;
    // Start is called before the first frame update
    void Start()
    {
        //aboutToBeHit = false;
        //starts the gameobject as invisible
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if a collision occurs, the object will reveal itself
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(aboutToBeHit)
        {
            Debug.Log("triggered invisible block");
            Reveal();
            aboutToBeHit = false;
        }
        //could put audio here for the reveal
    }

    public void IncomingHit()
    {
        aboutToBeHit = true;
    }
    */

    public void Reveal()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}

