using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    string newGameScene = "FpsScene";
    void Start()
    {

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
   
    public void ExitApplication()
    {
        Application.Quit();
    }
}
