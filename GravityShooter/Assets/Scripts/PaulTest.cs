using UnityEngine;
using System.Collections;

public class PaulTest : MonoBehaviour
{
    int health = 3;
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(health < 0)
            {
                health = 0;
                GUIManager.instance.Activate("UIPlayer", false);
                GUIMenuManager.GameOver();
            }
            else
                GUIManager.instance.ChangeHealth(health);

            health--;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            GUIMenuManager.PauseButton();
        }
	}
}
