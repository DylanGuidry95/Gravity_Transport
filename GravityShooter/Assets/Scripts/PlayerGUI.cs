using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] health;
    public Image shield;

    /// <summary>
    /// 0 is no health, which is HpBar_006
    /// 1 is one_third, which is HpBar_004
    /// 2 is half health, which is HpBar_002
    /// 3 is full health, which is HpBar_000
    /// 
    /// Also same with shields, but in odd numbers
    /// 0 = 006
    /// 1 = 005
    /// 2 = 003
    /// 1 = 001
    /// 
    /// In order to use PlayerGUI now, you must 
    /// make an instance of it, and then call in the function
    /// Remember to make the instance public and add it in from the hierarchy.
    /// </summary>
    /// <param name="hp"></param>

    public void HPChange(int hp)
    {
        imageRenderer.sprite = health[hp];
    }

    public void ShieldChange(bool set)
    {
        if(set == true)
        {
            shield.color = new Color(255, 255, 255, 255);
        }
        else
        {
            shield.color = new Color(255, 255, 255, 0);
        }
    }
}
