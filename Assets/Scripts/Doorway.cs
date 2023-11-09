using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] private Vector2 upPosition;
    [SerializeField] private Vector2 downPosition;
    [SerializeField] private Vector2 leftPosition;
    [SerializeField] private Vector2 rightPosition;
    private bool aboutToBeHit;
    private Vector2 directionOfPlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncomingHit()
    {
        aboutToBeHit = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 pPosition = player.transform.position;
        Vector2 position = transform.position;
        Vector2 relative = position - pPosition;
        directionOfPlayerMovement = relative.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aboutToBeHit)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            if (directionOfPlayerMovement == Vector2.up)
            {
                Debug.Log("up");
                camera.GetComponent<CameraMovement>().MoveCamera(upPosition);
            } else if (directionOfPlayerMovement == Vector2.down)
            {
                Debug.Log("down");
                camera.GetComponent<CameraMovement>().MoveCamera(downPosition);
            } else if (directionOfPlayerMovement == Vector2.left)
            {
                Debug.Log("left");
                camera.GetComponent<CameraMovement>().MoveCamera(leftPosition);
            } else if (directionOfPlayerMovement == Vector2.right)
            {
                Debug.Log("right");
                camera.GetComponent<CameraMovement>().MoveCamera(rightPosition);
            }
        }
    }
}
