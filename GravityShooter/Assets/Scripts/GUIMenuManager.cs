using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIMenuManager : MonoBehaviour
{
    void Awake()
    {
        GUIManager.instance.Activate("UITitle", true);
        GUIManager.instance.Activate("UIPlayButton", true);
        GUIManager.instance.Activate("UIOptionsButton", true);
        GUIManager.instance.Activate("UIQuitButton", true);
    }

    void Update()
    {
        PauseButton();
        GameOver();
    }

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

    public static void PauseButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GUIManager.instance.Activate("UIPauseText", true);
            GUIManager.instance.Activate("UIResumeButton", true);
            GUIManager.instance.Activate("UIQuitButton", true);
            GUIManager.instance.Activate("UIMainMenu", true);
        }
    }

    public static void GameOver()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GUIManager.instance.Activate("UIGameOver", true);
            GUIManager.instance.Activate("UIHighScores", true);
            GUIManager.instance.Activate("UICurrentScore", true);
            GUIManager.instance.Activate("UIQuitButton", true);
            GUIManager.instance.Activate("UIMainMenu", true);
        }
    }
}
