using UnityEngine;
using System.Collections;

public class PaulTest : MonoBehaviour
{
    int health = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GUIManager.instance.Activate("UIPlayer", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GUIManager.instance.Activate("UIPlayer", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GUIManager.instance.Activate("UIBoss", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GUIManager.instance.Activate("UIBoss", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GUIManager.instance.Activate("UIScore", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GUIManager.instance.Activate("UIScore", true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (health > 3)
            {
                health = 3;
                GUIManager.instance.Activate("UIPlayer", false);
            }
            else
                GUIManager.instance.ChangeHealth(health);

            health++;
        }
    }
}
