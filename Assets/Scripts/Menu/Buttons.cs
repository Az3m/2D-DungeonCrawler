using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public int GameStartScene;

    public void StartGame()
    {
        GameController.Health = 6;
        GameController.MoveSpeed = 5f;
        GameController.PlayerDamage = 1;
        GameController.FireRate = 0.5f;
        SceneManager.LoadScene(GameStartScene);
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;  
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
