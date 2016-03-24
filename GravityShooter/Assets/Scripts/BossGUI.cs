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
       // GUIManager.instance.TurnOff(bossSlider.gameObject);
       // GUIManager.instance.TurnOff(bossName.gameObject);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        GUIManager.instance.TurnOn(bossName.gameObject);
    //        GUIManager.instance.TurnOn(bossSlider.gameObject);
    //        Debug.Log(gameObject + "On");
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        GUIManager.instance.TurnOff(bossName.gameObject);
    //        GUIManager.instance.TurnOff(bossSlider.gameObject);
    //        Debug.Log(gameObject + "Off");
    //    }
    //}

    void bossGUI()
    {
        // When the boss appears, I need to turn on the bossSlider and bossName on.
        if (bossSlider.value == 0)
        {
            GUIManager.instance.TurnOff(bossSlider.gameObject);
            GUIManager.instance.TurnOff(bossName.gameObject);
        }
    }
}
