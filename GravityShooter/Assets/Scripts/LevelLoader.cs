using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Will go back to the main menu scene
    /// </summary>
    //public void BackMainMenuButton()
    //{
    //    SceneManager.LoadScene("GamePlay");
    //    SceneManager.LoadScene("MainMenu");
    //}

    public void Mainmenu()
    {
        GUIMenuManager.MainMenu();
    }

    public void GamePlay()
    {
        GUIMenuManager.PlayButton();
        SceneManager.LoadScene("GamePlayGUI");
        SceneManager.SetActiveScene("MainMenuGUI", false);
    }
}
