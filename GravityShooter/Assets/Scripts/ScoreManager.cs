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
        
        LoadScores();

        currentScore = 0;
        scoreText = GetComponent<Text>();
    }

    public static int IncreasScoreBy(int a_score)
    {
        currentScore += a_score;

        return 0;
    }

    private int SaveScores()
    {
        HighScore hs = new HighScore(scoreText.text, currentScore);

        highScores.Add(hs);
        highScores = highScores.OrderByDescending(x => x).ToList();

        if (highScores.Count > maxHighScores)
            highScores.RemoveRange(maxHighScores, highScores.Count - maxHighScores);

        Serializer.SerializeXML(highScores, "HighScore", @"..\");

        return 0;
    }

    private int LoadScores()
    {
        highScores = Serializer.DeserializeXML<List<HighScore>>(@"..\HighScore");
        highestScore = highScores[0].score;

        return 0;
    }

    // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING 
    void OnApplicationQuit()
    {
        SaveScores();
    }
    // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING // TESTING

    private static int m_currentScore;
    public string playerName;
    public static Text scoreText;

    public int maxHighScores;
    
    public List<HighScore> highScores = new List<HighScore>();

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
                scoreText.text = "Score: " + m_currentScore.ToString();
        }
    }
    
    [XmlRoot ("HighScores")]
    public class HighScore
    {
        public string name;
        public int score;

        public HighScore() { }
        public HighScore(string n, int s)
        {
            name = n;
            score = s;
        }
    }
}