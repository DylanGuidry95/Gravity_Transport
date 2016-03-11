using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BossGUI : MonoBehaviour
{
    /// <summary>
    /// Enemy Boss GUI slider needs to be handle on enemy side
    /// </summary>
    public Text bossName;
    public Slider bossSlider;
    public int damage = 10;

    void Awake()
    {
        GUIManager.instance.TurnOff(bossSlider.gameObject);
        GUIManager.instance.TurnOff(bossName.gameObject);
    }

    void bossGUI()
    {
        if (bossSlider.value == 0)
        {
            bossSlider.value -= damage;
            GUIManager.instance.TurnOff(bossSlider.gameObject);
            GUIManager.instance.TurnOff(bossName.gameObject);
        }
    }
}
