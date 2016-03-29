using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Will go back to the main menu scene
    /// </summary>

    GUIMenuManager loadingMenus;

    public void Mainmenu()
    {
        loadingMenus.MainMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void GamePlay()
    {
        loadingMenus.PlayButton();
        SceneManager.LoadScene("GamePlay");
    }
}
