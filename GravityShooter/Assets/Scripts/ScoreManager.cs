using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : Singleton<ScoreManager>
{
    protected override void Awake()
    {
        base.Awake();

        LoadScores();
        currentScore = 0;
    }

    public static int IncreasScoreBy(int a_score)
    {
        currentScore += a_score;

        return 0;
    }

    private int SaveScores()
    {
        highScores.Add(currentScore);
        highScores = highScores.OrderByDescending(x => x).ToList();

        if (highScores.Count > maxHighScores)
            highScores.RemoveRange(maxHighScores, highScores.Count - maxHighScores);

        Serializer.SerializeXML(highScores, "HighScore", @"..\");

        return 0;
    }

    private int LoadScores()
    {
        highScores = Serializer.DeserializeXML<List<int>>(@"..\HighScore");
        highestScore = highScores[0];

        return 0;
    }

    private static int m_currentScore;
    public static Text scoreText;

    public int maxHighScores = 3;
    public List<int> highScores = new List<int>();
    public int highestScore;

    public static int currentScore
    {
        get
        {
            return m_currentScore;
        }
        set
        {
            m_currentScore = value;
            if (scoreText)
                scoreText.text = m_currentScore.ToString();
        }
    }
}