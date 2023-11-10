using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    private int moves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoves(int no)
    {
        moves += no;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Moves done: " + moves.ToString();
    }

    public int GetMoves()
    {
        return moves;
    }

    public void SetMoves(int no)
    {
        moves = no;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Moves done: " + moves.ToString();
    }
}
