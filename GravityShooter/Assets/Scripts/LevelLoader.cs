using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Task: Set up a system to handle scene transitions 
    /// as the the user progresses through the game.
    /// 
    /// Pass in a string to call scene and what kind of mode = singlar/additive .
    /// </summary>

    public static void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
