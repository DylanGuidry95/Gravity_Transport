using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentScore : MonoBehaviour
{
    public Text currentScoreText;

    public void currentScore()
    {
        currentScoreText.text = "Current Score" + ScoreManager.score;
    }
}
