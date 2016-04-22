using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GUIMenuManager : MonoBehaviour
{
    /// <summary>
    /// Checks to see if the platform is build in WebGL.
    /// </summary>
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GUIManager.instance.Activate("UIQuitButton", false);
        }
    }
    
    /// <summary>
    /// Turning off gui elements to able to load scene to Main Menu
    /// </summary>
    public void MainMenu()
    {
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
        GUIManager.instance.Activate("UIMainMenu", false);
        GUIManager.instance.Activate("UIGameOver", false);
        GUIManager.instance.Activate("UIHighScores", false);
        GUIManager.instance.Activate("UICurrentScore", false);
        GameStates.ChangeState("MainMenu");
    }

    /// <summary>
    /// Turning off gui elements to able to load scene to GamePlay
    /// </summary>
    public void PlayButton()
    {
        GUIManager.instance.Activate("UIBackground", false);
        GUIManager.instance.Activate("UIPlayButton", false);
        GUIManager.instance.Activate("UIOptionsButton", false);
        GUIManager.instance.Activate("UICreditButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
        GameStates.ChangeState("Game");
    }

    /// <summary>
    /// Turns off GUI elements for audio gui elements
    /// </summary>
    public void OptionButton()
    {
            GUIManager.instance.Activate("UIPlayButton", false);
            GUIManager.instance.Activate("UIOptionsButton", false);
            GUIManager.instance.Activate("UICreditButton", false);
            GUIManager.instance.Activate("UIQuitButton", false);

            GUIManager.instance.Activate("UIAudioText", true);
            GUIManager.instance.Activate("UIMusicToggle", true);
            GUIManager.instance.Activate("UIMusicToggleSlider", true);
            GUIManager.instance.Activate("UISoundEffectsToggle", true);
            GUIManager.instance.Activate("UISoundEffectsSlider", true);
            GUIManager.instance.Activate("UIBackButton", true);
    }

    public void CreditButton()
    {
        GUIManager.instance.Activate("UIPlayButton", false);
        GUIManager.instance.Activate("UIOptionsButton", false);
        GUIManager.instance.Activate("UICreditButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);

        GUIManager.instance.Activate("UIProgrammers", true);
        GUIManager.instance.Activate("UIArtists", true);
        GUIManager.instance.Activate("UIBackButton", true);
    }

    /// <summary>
    /// If user wants to exit appilcation
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// Button just made to go backwards in Main Menu
    /// </summary>
    public void BackButton()
    {
        GUIManager.instance.Activate("UIPlayButton", true);
        GUIManager.instance.Activate("UIOptionsButton", true);
        GUIManager.instance.Activate("UICreditButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);

        GUIManager.instance.Activate("UIAudioText", false);
        GUIManager.instance.Activate("UIMusicToggle", false);
        GUIManager.instance.Activate("UIMusicToggleSlider", false);
        GUIManager.instance.Activate("UISoundEffectsToggle", false);
        GUIManager.instance.Activate("UISoundEffectsSlider", false);
        GUIManager.instance.Activate("UIProgrammers", false);
        GUIManager.instance.Activate("UIArtists", false);
        GUIManager.instance.Activate("UIBackButton", false);
    }

    /// <summary>
    /// PauseButton will pause the game play
    /// </summary>
    public static void PauseButton()
    {
        GUIManager.instance.Activate("UIPauseText", true);
        GUIManager.instance.Activate("UIResumeButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);
        GUIManager.instance.Activate("UIMainMenu", true);
    }

    /// <summary>
    /// To go back to the gameplay
    /// </summary>
    public static void ResumeButton()
    {
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
        GUIManager.instance.Activate("UIMainMenu", false);
    }

    public void HitResumeButton()
    {
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIQuitButton", false);
        GUIManager.instance.Activate("UIMainMenu", false);
        GameStates.PauseGame();
    }

    /// <summary>
    /// Should be called when user completed level or player died, also load GameOver Scene
    /// </summary>
    public static void GameOver()
    {
        GUIManager.instance.Activate("UIPlayer", false);
        GUIManager.instance.Activate("UIBoss", false);
        GUIManager.instance.Activate("UICreditButton", false);
        GUIManager.instance.Activate("UIScore", false);
    }
}
