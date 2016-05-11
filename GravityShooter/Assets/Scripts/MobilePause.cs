using UnityEngine;
using System.Collections;

public class MobilePause : MonoBehaviour
{
    void Start()
    {
        if(!Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
    }
}
