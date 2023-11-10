using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private float time;
    private bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            time += Time.deltaTime;
            gameObject.GetComponent<TextMeshProUGUI>().text = "Time spent: " + Mathf.FloorToInt(time).ToString();
        } 
    }


    public float StopTime()
    {
        stopped = true;
        return time;
    }
}
