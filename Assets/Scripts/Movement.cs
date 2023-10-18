using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 moving = new Vector2(h, v);

            if((h != 0) != (v != 0))
            {
                StartCoroutine(Move(moving));
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
