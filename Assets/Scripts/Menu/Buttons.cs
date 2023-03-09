using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public int GameStartScene;

    public void StartGame()
    {
        SceneManager.LoadScene(GameStartScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
