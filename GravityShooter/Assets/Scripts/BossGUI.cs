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
    public static int damage = 10;

    public void ToggleBossGUI(bool state)
    {
        GUIManager.instance.Activate("UIBoss", state);
    }

    public void HPChange(int dmg)
    {
        bossSlider.value = dmg;
    }
}
