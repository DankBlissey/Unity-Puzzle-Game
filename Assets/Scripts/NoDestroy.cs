using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("UiCanvas");
        if(obj.Length > 1 && gameObject.tag == "UiCanvas")
        {
            Destroy(gameObject);
        }
        GameObject[] obj1 = GameObject.FindGameObjectsWithTag("music");
        if (obj1.Length > 1 && gameObject.tag == "music")
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
