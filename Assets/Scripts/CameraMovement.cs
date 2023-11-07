using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(Vector2 position)
    {
        Debug.Log("Move camera command received");
        StartCoroutine(Move(position));
    }

    private IEnumerator Move(Vector2 position)
    {
        Vector3 endPosition = new Vector3(position.x, position.y, -1);
        Vector3 startPos = transform.position;
        float elapsedTime = 0;
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveTime;
            transform.position = Vector3.Lerp(startPos, endPosition, percent);
            yield return null;
        }
        transform.position = endPosition;
    }
}
