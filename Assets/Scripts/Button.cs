using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private bool Rotate1;
    [SerializeField] private bool Rotate2;
    [SerializeField] private bool Rotate3;
    [SerializeField] private bool Move1;
    [SerializeField] private bool Move2;
    [SerializeField] private bool Move3;
    private bool aboutToBeHit;

    // Start is called before the first frame update
    void Start()
    {
        aboutToBeHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncomingHit()
    {
        aboutToBeHit = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (aboutToBeHit)
        {
            Debug.Log("triggered button");
            if (Rotate1)
            {
                foreach(GameObject block in (GameObject.FindGameObjectsWithTag("Rotate1")))
                {
                    Debug.Log("telling object to toggle");
                    block.GetComponent<Rotate>().RecieveToggle();
                }
            }
            if(Rotate2)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Rotate2")))
                {
                    block.GetComponent<Rotate>().RecieveToggle();
                }
            }
            if(Rotate3)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Rotate3")))
                {
                    block.GetComponent<Rotate>().RecieveToggle();
                }
            }
            if(Move1)
            {

            }
            if (Move2)
            {

            }
            if (Move3)
            {

            }
            aboutToBeHit = false;
        }
        //could put audio here for the reveal
    }
}
