using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector2 location;
    [SerializeField] private float waitTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(aboutToBeHit == true)
        {
            Debug.Log("Collision with teleporter detected");
            GameObject hitObject = collision.gameObject;
            hitObject.GetComponent<Movement>().ThingsMoving();
            hitObject.GetComponent<TrailRenderer>().emitting = false;
            Vector2 directionOfObject = hitObject.transform.position - transform.position;
            hitObject.GetComponent<Movement>().Teleport(location, directionOfObject * -1);
            aboutToBeHit = false;
        }
        */
    }

    public void Activate(Vector2 dir)
    {
        if(aboutToBeHit == true)
        {
            Debug.Log("Collision with teleporter detected");
            GameObject hitObject = GameObject.FindGameObjectWithTag("Player");
            hitObject.GetComponent<Movement>().ThingsMoving();
            //hitObject.GetComponent<TrailRenderer>().emitting = false;
            Vector2 directionOfObject = new Vector3(dir.x,dir.y) - transform.position;
            directionOfObject = directionOfObject.normalized;
            hitObject.GetComponent<Movement>().Teleport(location, directionOfObject * -1);
            aboutToBeHit = false;
        }
    }

    public void IncomingHit()
    {
        aboutToBeHit = true;
        StartCoroutine(Wait(waitTime));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        aboutToBeHit = false;
    }
}
