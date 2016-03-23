using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public Image imageRenderer;
    //public Sprite[] playerGUI;
    public Sprite[] health;
    public Sprite[] shield;

    /// <summary>
    /// When the player gets hit/damage, the screen will flash red.
    /// </summary>

    void Awake()
    {
        GUIManager.instance.TurnOn(gameObject);
    }
 
    //0 is 0/3
    //1 is 1/3
    //2 is 2/3
    //3 is 3/3
    public void HPChange(int hp)
    {
        imageRenderer.sprite = health[hp];
    }

    public void ShieldChange(int shields)
    {
        imageRenderer.sprite = shield[shields];
    }

 
}
