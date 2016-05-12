using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{
    public List<Sprite> controlImage = new List<Sprite>();
    public UnityEngine.UI.Button AndroidPause;
	// Use this for initialization
	void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = controlImage[1];
            AndroidPause.gameObject.SetActive(true);

        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().sprite = controlImage[0];
            AndroidPause.gameObject.SetActive(false);
        }


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
        if (Time.timeScale == 0 && Application.platform == RuntimePlatform.Android)
            AndroidPause.gameObject.SetActive(false);
        else if (Time.timeScale != 0 && Application.platform == RuntimePlatform.Android)
            AndroidPause.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        GameStates.ChangeState("Pause");
    }
}
