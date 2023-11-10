using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextDissapear : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        Debug.Log("Text hide called");
        //gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
        StartCoroutine(Fade(1, 0));
        
    }
    private IEnumerator Fade(float start, float end)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / fadeDuration;
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(start, end, percent));
            yield return null;
        }
    }
}
