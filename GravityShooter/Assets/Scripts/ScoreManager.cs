using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    // Dodging = 10, small enemies = 20, medium enemies = 30, large = 50, bosses = 100
    public Text scoreText;
    public Text bossName;
    public Slider bossSlider;
    public Sprite playerHealth;
    public static int score;
    public int scoreValue = 10;
    public int damage = 10;

    void Awake()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;

        // When the enemy dies, the score will increase
        if (Input.GetKeyDown(KeyCode.A))
        {
            //playerHealth.
        }
    }
}

//if (bossSlider.value == 0)
//{
//    bossSlider.value -= damage;
//    bossSlider.gameObject.SetActive(false);
//    bossName.gameObject.SetActive(false);
//}
