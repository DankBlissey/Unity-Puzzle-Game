using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private int level;
    // Start is called before the first frame update
    void Start()
    {
        int previousLevel = level - 1;
        GameObject buttonText = transform.GetChild(0).gameObject;
        GameObject bestText = transform.GetChild(1).gameObject;
        if (PlayerPrefs.GetInt("Level" + previousLevel.ToString() + "Moves", 0) == 0 && level != 1)
        {
            gameObject.GetComponent<Button>().interactable = false;
            buttonText.GetComponent<TextMeshProUGUI>().text = "Locked";
            bestText.GetComponent<TextMeshProUGUI>().enabled = false;


        } else
        {
            int bestMoves = PlayerPrefs.GetInt("Level" + level.ToString() + "Moves");
            float bestTime = PlayerPrefs.GetFloat("Level" + level.ToString() + "Time");

            gameObject.GetComponent<Button>().interactable = true;
            buttonText.GetComponent<TextMeshProUGUI>().text = "Level " + level.ToString();
            bestText.GetComponent<TextMeshProUGUI>().text = "Best Time: " + bestTime.ToString("F2") + "\n" + "Best Moves: " + bestMoves.ToString();
            bestText.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
