using UnityEngine;
using System.Collections;

public class RegisterGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GUIManager.instance.Register(name, gameObject);	
	}
	

}
