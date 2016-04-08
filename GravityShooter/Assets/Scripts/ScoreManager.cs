using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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

    public int maxHighScores = 3;
    public List<int> highScores = new List<int>();
    public int highestScore;

    void Awake()
    {
        LoadScores();
        score = 0;
    }

    void Update()
    {
        if(scoreText)
            scoreText.text = "Score: " + score;
    }

    void OnApplicationQuit()
    {
        SaveScores();

        Serializer.SerializeXML(highScores, "HighScore", @"..\");
    }

    private int SaveScores()
    {
        highScores.Add(score);
        highScores = highScores.OrderByDescending(x => x).ToList();

        if (highScores.Count > maxHighScores)
            highScores.RemoveRange(maxHighScores, highScores.Count - maxHighScores);

        return 0;
    }

    private int LoadScores()
    {
        highScores = Serializer.DeserializeXML<List<int>>(@"..\HighScore");
        highestScore = highScores[0];

        return 0;
    }
}