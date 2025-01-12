using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    [SerializeField] private string level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteLevel()
    {
        int moves = GameObject.FindGameObjectWithTag("Moves").GetComponent<MoveCounter>().GetMoves();
        float time = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeCounter>().StopTime();
        if(moves < PlayerPrefs.GetInt("Level" + level + "Moves", 10000))
        {
            PlayerPrefs.SetInt("Level" + level + "Moves", moves);
        }
        if(time < PlayerPrefs.GetFloat("Level" + level + "Time", 10000))
        {
            PlayerPrefs.SetFloat("Level" + level + "Time", time);
        }
        Destroy(GameObject.FindGameObjectWithTag("UiCanvas"));
        PlayerPrefs.Save();
    }
}
