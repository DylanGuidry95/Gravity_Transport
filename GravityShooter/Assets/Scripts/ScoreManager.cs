using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : Singleton<ScoreManager>
{
    protected override void Awake()
    {
        base.Awake();

        currentScore = 0;
        scoreText = GetComponent<Text>();
    }

    public static int IncreasScoreBy(int a_score)
    {
        currentScore += a_score;

        return 0;
    }

    void Update()
    {
        if(GameStates.ExitGamePlay)
        {
            highScores.Sort();
            highScores.Reverse();
        }
    }

    public string playerName;
    public static Text scoreText;

    public int highestScore;
    public List<int> highScores = new List<int>();

    private static int m_currentScore;
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
                scoreText.text = "Score: " + m_currentScore.ToString();
        }
    }
}