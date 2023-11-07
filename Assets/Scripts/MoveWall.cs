using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    [SerializeField] private Vector2 position1;
    [SerializeField] private Vector2 position2;
    private bool isMoved;
    // Start is called before the first frame update
    void Start()
    {
        isMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveToggle(float speed)
    {
        Debug.Log("Move command received");
        StartCoroutine(ToggleMovement(speed));
    }

    private IEnumerator ToggleMovement(float speed)
    {
        if (isMoved == false)
        {
            float elapsedTime = 0;
            while (elapsedTime < speed)
            {
                elapsedTime += Time.deltaTime;
                float percent = elapsedTime / speed;
                transform.position = Vector2.Lerp(position1, position2, percent);
                yield return null;
            }
            Vector2 hitObj = transform.position;
            hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
            transform.position = hitObj;
            isMoved = true;
        } else
        {
            float elapsedTime = 0;
            while (elapsedTime < speed)
            {
                elapsedTime += Time.deltaTime;
                float percent = elapsedTime / speed;
                transform.position = Vector2.Lerp(position2, position1, percent);
                yield return null;
            }
            Vector2 hitObj = transform.position;
            hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
            transform.position = hitObj;
            isMoved = false;
        }
    }
}
