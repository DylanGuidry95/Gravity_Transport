using UnityEngine;
using System.Collections;

public class MobilePause : MonoBehaviour
{
    void Start()
    {
        isPaused = false;
        if(!Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    bool isPaused;
}
