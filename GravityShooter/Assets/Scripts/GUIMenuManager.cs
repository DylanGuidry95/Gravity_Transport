using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GUIMenuManager : MonoBehaviour
{
    /// <summary>
    /// Main Menu by just activing some GUI elements
    /// </summary>
    public void MainMenu()
    {
        LevelLoader.LoadLevel("MainMenu", LoadSceneMode.Single);
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
        GUIManager.instance.Activate("UIMainMenu", false);
        GUIManager.instance.Activate("UIGameOver", false);
        GUIManager.instance.Activate("UIHighScores", false);
        GUIManager.instance.Activate("UICurrentScore", false);
    }

    /// <summary>
    /// When the play button is called, Game Play scene will be loaded in
    /// </summary>
    public void PlayButton()
    {
        LevelLoader.LoadLevel("GamePlay", LoadSceneMode.Single);
        GUIManager.instance.Activate("UITitle", false);
        GUIManager.instance.Activate("UIPlayButton", false);
        GUIManager.instance.Activate("UIOptionsButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
    }

    /// <summary>
    /// When the Optiton Button is called, gui elements just turn on/off for audio
    /// </summary>
    public void OptionButton()
    {
            GUIManager.instance.Activate("UIPlayButton", false);
            GUIManager.instance.Activate("UIOptionsButton", false);
            GUIManager.instance.Activate("UIQuitButton", false);

            GUIManager.instance.Activate("UIAudioText", true);
            GUIManager.instance.Activate("UIMusicToggle", true);
            GUIManager.instance.Activate("UIMusicToggleSlider", true);
            GUIManager.instance.Activate("UISoundEffectsToggle", true);
            GUIManager.instance.Activate("UISoundEffectsSlider", true);
            GUIManager.instance.Activate("UIBackButton", true);
    }

    /// <summary>
    /// If user wants to exit appilcation, and checks if it's build in WebGL
    /// </summary>
    public void QuitButton()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GUIManager.instance.Activate("UIQuitButton", false);
        }
        else
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Button just made to go backwards in Main Menu
    /// </summary>
    public void BackButton()
    {
        GUIManager.instance.Activate("UIPlayButton", true);
        GUIManager.instance.Activate("UIOptionsButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);

        GUIManager.instance.Activate("UIAudioText", false);
        GUIManager.instance.Activate("UIMusicToggle", false);
        GUIManager.instance.Activate("UIMusicToggleSlider", false);
        GUIManager.instance.Activate("UISoundEffectsToggle", false);
        GUIManager.instance.Activate("UISoundEffectsSlider", false);
        GUIManager.instance.Activate("UIBackButton", false);
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIGameOver", false);
        GUIManager.instance.Activate("UIHighScores", false);
    }

    /// <summary>
    /// PauseButton will pause the game play
    /// Needs to be checked every frame in gameplay scene
    /// </summary>
    public static void PauseButton()
    {
        GUIManager.instance.Activate("UIPauseText", true);
        GUIManager.instance.Activate("UIResumeButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);
        GUIManager.instance.Activate("UIMainMenu", true);
    }

    /// <summary>
    /// Should be called when user completed level or has died
    /// </summary>
    public static void GameOver()
    {
        GUIManager.instance.Activate("UIGameOver", true);
        GUIManager.instance.Activate("UIHighScores", true);
        GUIManager.instance.Activate("UICurrentScore", true);
        GUIManager.instance.Activate("UIQuitButton", true);
        GUIManager.instance.Activate("UIMainMenu", true);
    }
}
