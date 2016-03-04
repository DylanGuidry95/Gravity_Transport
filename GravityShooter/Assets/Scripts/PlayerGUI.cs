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
        imageRenderer.sprite = playerGUI[hp];
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    hpBar++;
        //    if (hpBar >= playerGUI.Length)
        //    {
        //        hpBar = 3;
        //    }
    }
    
}
