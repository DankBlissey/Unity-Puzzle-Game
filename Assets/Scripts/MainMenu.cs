using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

}
