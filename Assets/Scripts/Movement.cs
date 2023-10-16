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
        System.Func<KeyCode, bool> movement;
        movement = Input.GetKeyDown;

        if (movement(KeyCode.UpArrow))
        {
            StartCoroutine(Move(Vector2.up));
        } else if (movement(KeyCode.DownArrow))
        {
            StartCoroutine(Move(Vector2.down));
        } else if (movement(KeyCode.LeftArrow))
        {
            StartCoroutine(Move(Vector2.left));
        } else if (movement(KeyCode.RightArrow))
        {
            StartCoroutine(Move(Vector2.right));
        }

        
    }

    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPos = transform.position;

        Vector2 endPos = startPos + (direction * gridSize);

        float elapsedTime = 0;

        while(elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPos, endPos, percent);
            yield return null;
        }

        transform.position = endPos;


        isMoving = false;
    }


}
