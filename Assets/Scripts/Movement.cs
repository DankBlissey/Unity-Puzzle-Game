using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            System.Func<KeyCode, bool> movement;
            movement = Input.GetKey;

            if (movement(KeyCode.UpArrow))
            {
                StartCoroutine(Move(Vector2.up));
            }
            else if (movement(KeyCode.DownArrow))
            {
                StartCoroutine(Move(Vector2.down));
            }
            else if (movement(KeyCode.LeftArrow))
            {
                StartCoroutine(Move(Vector2.left));
            }
            else if (movement(KeyCode.RightArrow))
            {
                StartCoroutine(Move(Vector2.right));
            }
        }

    }

    private IEnumerator Move(Vector2 direction)
    {
        Vector2 adj = direction * 0.5f * gridSize;
       
        
        isMoving = true;

        Vector2 startPos = transform.position;

        Vector2 hitObj = startPos;

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 100f);

        if(hit)
        {
            Debug.Log("hit object at " + hit.point);
            hitObj = hit.point - adj;
        }

        Vector2 endPos = startPos + (direction * gridSize);

        float elapsedTime = 0;

        while(elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPos, hitObj, percent);
            yield return null;
        }

        transform.position = hitObj;


        isMoving = false;
    }


}
