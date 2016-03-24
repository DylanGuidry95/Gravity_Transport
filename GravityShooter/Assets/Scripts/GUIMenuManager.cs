using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIMenuManager : MonoBehaviour
{
/// <summary>
/// When the game starts, the main menu shows up
/// </summary>
    void Awake()
    {
        GUIManager.instance.Activate("UITitle", true);
        GUIManager.instance.Activate("UIPlayButton", true);
        GUIManager.instance.Activate("UIOptionsButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);
    }

    /// <summary>
    /// When the play button is called, the main menu will disappear and UI interface
    /// will appear.
    /// </summary>
    public void PlayButton()
    {
            GUIManager.instance.Activate("UITitle", false);
            GUIManager.instance.Activate("UIPlayButton", false);
            GUIManager.instance.Activate("UIOptionsButton", false);
            GUIManager.instance.Activate("UIQuitButton", false);

            GUIManager.instance.Activate("UIPlayer", true);
            GUIManager.instance.Activate("UIBoss", true);
            GUIManager.instance.Activate("UIScore", true);
    }

    /// <summary>
    /// If the user hits the options, mainly it's just Audio/sound effects
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
            GUIManager.instance.Activate("UIMainMenu", true);
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
    /// Back to Main Menu will always go back to the Main menu, turning off all 
    /// other GUI elements and just restore back to original state
    /// </summary>
    public void BackMainMenuButton()
    {
        GUIManager.instance.Activate("UIPlayButton", true);
        GUIManager.instance.Activate("UIOptionsButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);

        GUIManager.instance.Activate("UIPlayer", false);
        GUIManager.instance.Activate("UIBoss", false);
        GUIManager.instance.Activate("UIScore", false);
        GUIManager.instance.Activate("UIAudioText", false);
        GUIManager.instance.Activate("UIMusicToggle", false);
        GUIManager.instance.Activate("UIMusicToggleSlider", false);
        GUIManager.instance.Activate("UISoundEffectsToggle", false);
        GUIManager.instance.Activate("UISoundEffectsSlider", false);
        GUIManager.instance.Activate("UIMainMenu", false);
        GUIManager.instance.Activate("UIPauseText", false);
        GUIManager.instance.Activate("UIResumeButton", false);
        GUIManager.instance.Activate("UIGameOver", false);
        GUIManager.instance.Activate("UICurrentScore", false);
        GUIManager.instance.Activate("UIHighScores", false);
    }

    /// <summary>
    /// Pause button will need to be checked every frame
    /// We will also need to check if the user presses the button during 
    /// main menus and etc.
    /// just call GUIMenuManager.PauseButton(), and that should work.
    /// </summary>
    public static void PauseButton()
    {
        GUIManager.instance.Activate("UIPauseText", true);
        GUIManager.instance.Activate("UIResumeButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);
        GUIManager.instance.Activate("UIMainMenu", true);
    }

    /// <summary>
    /// GameOver should be called when the player is dead,
    /// Use GUIMenuManager.GameOver(), and it work just fine.
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
