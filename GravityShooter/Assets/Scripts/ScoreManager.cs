using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    // Dodging = 10, 
    // small enemies = 20, 
    // medium enemies = 30, 
    //large = 50, 
    //bosses = 100

    public static int score;
    public int scoreDodge = 10;
    public int scoreSmallEnemy = 20;
    public int scoreMediumEnemy = 30;
    public int scoreLargeEnemy = 50;
    public int scoreBoss = 100;
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

/// <summary>
/// Enemy Boss GUI slider needs to be handle on enemy side
/// </summary>
//public Text bossName;
//public Slider bossSlider;
//public int damage = 10;
//if (bossSlider.value == 0)
//{
//    bossSlider.value -= damage;
//    bossSlider.gameObject.SetActive(false);
//    bossName.gameObject.SetActive(false);
//}
