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
    [SerializeField] private float ChangeSpeed;
    private bool aboutToBeHit;
    [SerializeField] private AudioClip buttonPress;

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
        
        //could put audio here for the reveal
    }

    public void Activate()
    {
        if (aboutToBeHit)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Movement>().ThingsMoving();
            Debug.Log("triggered button");
            AudioSource source = gameObject.GetComponent<AudioSource>();
            source.PlayOneShot(buttonPress);
            if (Rotate1)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Rotate1")))
                {
                    Debug.Log("telling object to toggle");
                    block.GetComponent<Rotate>().RecieveToggle(ChangeSpeed);
                }
            }
            if (Rotate2)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Rotate2")))
                {
                    block.GetComponent<Rotate>().RecieveToggle(ChangeSpeed);
                }
            }
            if (Rotate3)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Rotate3")))
                {
                    block.GetComponent<Rotate>().RecieveToggle(ChangeSpeed);
                }
            }
            if (Move1)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Move1")))
                {
                    block.GetComponent<MoveWall>().RecieveToggle(ChangeSpeed);
                }
            }
            if (Move2)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Move2")))
                {
                    block.GetComponent<MoveWall>().RecieveToggle(ChangeSpeed);
                }
            }
            if (Move3)
            {
                foreach (GameObject block in (GameObject.FindGameObjectsWithTag("Move3")))
                {
                    block.GetComponent<MoveWall>().RecieveToggle(ChangeSpeed);
                }
            }
            aboutToBeHit = false;
            //float dur = player.GetComponent<Movement>().getMoveDuration();
            //StartCoroutine(Wait(ChangeSpeed - 0.5f *dur, player));
            StartCoroutine(Wait(ChangeSpeed, player));
        }
    }

    private IEnumerator Wait(float time, GameObject player)
    {
        if(time >= 0)
        {
            yield return new WaitForSeconds(time);
        } else
        {
            yield return new WaitForSeconds(ChangeSpeed);
        }
        
        player.GetComponent<Movement>().ThingsMoved();
    }
}
