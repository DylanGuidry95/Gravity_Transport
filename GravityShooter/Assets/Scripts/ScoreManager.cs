using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int scoreSmallEnemy = 5;
    public static int scoreMediumEnemy = 10;
    public static int scoreLargeEnemy = 15;
    public static int scoreBoss = 20;
    public Text scoreText;
   
    void Awake()
    { 
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}