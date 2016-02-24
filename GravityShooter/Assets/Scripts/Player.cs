using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    void Awake()
    {
        Messenger.AddListener<string>("UserInput", ActionTriggered);
    }
	// Use this for initialization
	void Start ()
    {
        AddControls();
	}
	
    void AddControls()
    {
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("AddControl", KeyCode.LeftShift, "Run", INPUT_DEVICE.KEYBOARD);
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("AddControl", KeyCode.Mouse0, "Shoot", INPUT_DEVICE.KEYBOARD);
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("AddControl", KeyCode.W, "Walk", INPUT_DEVICE.KEYBOARD);
    }

    void ActionTriggered(string action)
    {
        switch(action)
        {
            case "Shoot":
                Debug.Log("Shooting");
                break;
            case "Run":
                Debug.Log("Runing");
                break;
            case "Walk":
                Debug.Log("Walking");
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
