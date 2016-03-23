using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] playerGUI;

    /// <summary>
    /// When the player gets hit/damage, the screen will flash red.
    /// </summary>

    void Awake()
    {
        GUIManager.instance.TurnOn(imageRenderer.gameObject);
    }

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.A))
    //    {
    //        GUIManager.instance.TurnOn(gameObject);
    //        Debug.Log(gameObject + "On");
    //    }
            
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        GUIManager.instance.TurnOff(gameObject);
    //        Debug.Log(gameObject + "Off");
    //    }  
    //}

    public void PlayerBarGUI(int hp)
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
    }
}
