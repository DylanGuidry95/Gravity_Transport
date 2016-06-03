// ERIC MOULEDOUX
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
        scoreText = GetComponent<Text>();
        base.Awake();

        currentScore = 0;

        highScores = new List<int>();
        string scores = PlayerPrefs.GetString("highScores");

        for (int i = 0, j = 0; i < scores.Length; i++)
        {
            if (scores[i] == ' ')
            {
                int x = 0;
                int.TryParse(scores.Substring(j, i - j), out x);
                highScores.Add(x);
                j = i + 1;
            }
        }
        highestScore = highScores.Count > 0 ? highScores[0] : 0;
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
            highScores.Add(currentScore);
            highScores.Sort();
            if (highScores.Count > 10) highScores.RemoveAt(0);
            highScores.Reverse();

            string scores = "";
            foreach(int i in highScores)
            {
                scores += i.ToString() + " ";
            }

            PlayerPrefs.SetString("highScores", scores);
        }
    }
    
    static Text scoreText;

    public int highestScore;
    public List<int> highScores;

    private static int m_currentScore;
    static int currentScore
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