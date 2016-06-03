using UnityEngine;

public static class VolumeLevels
{
    public static float Master
    {
        get
        {
            return PlayerPrefs.GetFloat("masterVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("masterFloat", Mathf.Clamp(value, 0, 1));
        }
    }
    public static float Music
    {
        get
        {
            return PlayerPrefs.GetFloat("musicVolume") * PlayerPrefs.GetFloat("masterVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("musciVolume", Mathf.Clamp(value, 0, 1));
        }
    }
    public static float Effects
    {
        get
        {
            return PlayerPrefs.GetFloat("effectsVolume") * PlayerPrefs.GetFloat("masterVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("effectsVolume", Mathf.Clamp(value, 0, 1));
        }
    }
}
