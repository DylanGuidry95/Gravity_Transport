using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] health;
    public Sprite[] shield;

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
    /// </summary>
    /// <param name="hp"></param>

    public void HPChange(int hp)
    {
        imageRenderer.sprite = health[hp];
    }

    public void ShieldChange(int shields)
    {
        imageRenderer.sprite = shield[shields];
    }
}
