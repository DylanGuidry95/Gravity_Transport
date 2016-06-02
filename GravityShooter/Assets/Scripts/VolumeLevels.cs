using UnityEngine;

public static class VolumeLevels
{
    private static float m_music = 1;
    private static float m_effects = 1;
    private static float m_master = 1;

    public static float Master
    {
        get { return m_master; }
        set { m_master = Mathf.Clamp(value, 0, 1); }
    }
    public static float Music
    {
        get { return m_music * m_master; }
        set { m_music = Mathf.Clamp(value, 0, 1); }
    }
    public static float Effects
    {
        get { return m_effects * m_master; }
        set { m_effects = Mathf.Clamp(value, 0, 1); }
    }
}
