using UnityEngine;
using System.Collections;

public class RegisterGUI : MonoBehaviour
{
	void Awake()
    {
        GUIManager.instance.Register(name, gameObject);	
	}
}
