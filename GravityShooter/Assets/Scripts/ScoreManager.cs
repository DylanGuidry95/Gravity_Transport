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

    public List<int> highScores = new List<int>();
    public int highestScore;

    void Awake()
    {
        highScores = Serializer.DeserializeXML<List<int>>(@"..\HighScore");
        score = 0;
    }

    void Update()
    {
        if(scoreText)
            scoreText.text = "Score: " + score;
    }

    void OnApplicationQuit()
    {
        Serializer.SerializeXML(highScores, "HighScore", @"..\");
    }
}