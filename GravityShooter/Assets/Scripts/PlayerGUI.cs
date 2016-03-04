using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] playerHealth;
    int hpBar = 0;

    void Update()
    {
        PlayerBarGUI();
    }

    void PlayerBarGUI()
    {
        spriteRenderer.sprite = playerHealth[hpBar];
        if (Input.GetKeyDown(KeyCode.D))
        {
            hpBar++;
            if (hpBar >= playerHealth.Length)
            {
                hpBar = 0;
            }
        }
    }

    
}
