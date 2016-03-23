using UnityEngine;
using System.Collections;

public class PaulTest : MonoBehaviour
{
    bool positive = false;
    int health = 3;
    void Awake()
    {
        
    }
    // Update is called once per frame
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
        if (Input.GetKey(KeyCode.Return))
        {
            if (health >= 3)
                positive = true;
            else if (health <= 0)
                positive = false;
            if (positive)
                health--;
            if (!positive)
                health++;
            
                

            GUIManager.instance.ChangeHealth(health);

        }
    }
}
