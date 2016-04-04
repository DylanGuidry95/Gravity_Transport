using UnityEngine;
using System.Collections;

public class RegisterGUI : MonoBehaviour
{
    /// <summary>
    /// Attach this to GUI element and let them
    /// add themselves to dictonary. 
    /// </summary>
	void Awake()
    {
        GUIManager.instance.Register(name, gameObject);	
	}
}
