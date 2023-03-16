using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public int GameEndScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(GameEndScene);
    }
}
