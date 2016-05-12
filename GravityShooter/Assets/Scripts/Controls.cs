using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{
    public List<Sprite> controlImage = new List<Sprite>();
	// Use this for initialization
	void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
            GetComponent<UnityEngine.UI.Image>().sprite = controlImage[1];
        else
            GetComponent<UnityEngine.UI.Image>().sprite = controlImage[0];

        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

	}
}
