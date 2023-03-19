using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        scoreText.SetText(score.ToString());
    }


}
