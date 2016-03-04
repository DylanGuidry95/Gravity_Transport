using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerGUI : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] playerGUI;
    int hpBar = 0;

    void Awake()
    {
        Messenger.AddListener<int>("Player took damage", PlayerBarGUI);
        Messenger.AddListener<int>("Player Created", PlayerBarGUI);
    }

    void PlayerBarGUI(int hp)
    {

        switch (hp)
        {
            case 1:
                imageRenderer.sprite = playerGUI[2];
                break;
            case 2:
                imageRenderer.sprite = playerGUI[1];
                break;
            case 3:
                imageRenderer.sprite = playerGUI[0];
                break;
            case 0:
                imageRenderer.sprite = playerGUI[3];
                break;
        }
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    hpBar++;
        //    if (hpBar >= playerGUI.Length)
        //    {
        //        hpBar = 3;
        //    }
    }
    
}
