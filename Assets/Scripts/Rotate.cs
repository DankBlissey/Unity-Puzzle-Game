using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private int setRotation;
    [SerializeField] private int rotation1;
    [SerializeField] private int rotation2;
    private bool isRotated;
    // Start is called before the first frame update
    void Start()
    {
        isRotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveToggle(float speed)
    {
        Debug.Log("Toggle command recieved");
        StartCoroutine(ToggleRotate(speed));
    }

    private IEnumerator ToggleRotate(float speed)
    {
        if(isRotated == false)
        {
            Quaternion initialRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0,0,rotation2));
            float elapsedTime = 0;

            while (elapsedTime < speed)
            {
                elapsedTime += Time.deltaTime;
                float percent = elapsedTime / speed;
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, percent);
                yield return null;
            }
            Vector2 hitObj = transform.position;
            hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
            transform.position = hitObj;
            isRotated = true;
        } else
        {
            Quaternion initialRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, rotation1));
            float elapsedTime = 0;

            while (elapsedTime < speed)
            {
                elapsedTime += Time.deltaTime;
                float percent = elapsedTime /speed;
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, percent);
                yield return null;
            }
            Vector2 hitObj = transform.position;
            hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
            transform.position = hitObj;
            isRotated = false;
        }
    }
}
