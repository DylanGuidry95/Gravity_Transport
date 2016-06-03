using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Text))]

public class HighScores : MonoBehaviour
{

	// Use this for initialization
	void Awake()
    {
        string scores = PlayerPrefs.GetString("highScores");
        string highs = "High Scores\n";

        for (int i = 0, j = 0; i < scores.Length; i++)
        {
            if (scores[i] == ' ')
            {
                highs += scores.Substring(j, i - j) + "\n";
                j = i + 1;
            }
        }


        GetComponent<Text>().text = highs;

    }
}
