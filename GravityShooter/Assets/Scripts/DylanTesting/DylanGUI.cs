using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DylanGUI : Singleton<DylanGUI>
{  
    protected override void Awake()
    {
        base.Awake();        
    }


    DylanMainMenu MainMenuUI;
    DylanGamePlay GamePlayUI;
    DylanGameOver GameOverUI;


    // Use this for initialization
    void Start()
    {
        MainMenuUI = GetComponentInChildren<DylanMainMenu>();
        GamePlayUI = GetComponentInChildren<DylanGamePlay>();
        GameOverUI = GetComponentInChildren<DylanGameOver>();

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenuUI.gameObject.SetActive(true);
            GamePlayUI.gameObject.SetActive(false);
            GameOverUI.gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GamePlayUI.gameObject.SetActive(true);
            MainMenuUI.gameObject.SetActive(false);
            GameOverUI.gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameOverUI.gameObject.SetActive(true);
            GamePlayUI.gameObject.SetActive(false);
            MainMenuUI.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
