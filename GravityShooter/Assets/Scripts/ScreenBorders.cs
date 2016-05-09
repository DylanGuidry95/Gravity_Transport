using UnityEngine;
using System.Collections;

public class ScreenBorders
{
    private static Camera camera = Camera.main; // The main camera in the scene

    /// <summary>
    /// Top left corner of the screen in 3d world space
    /// </summary>
    public static Vector3 m_topLeft = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 0));
    /// <summary>
    /// Top right corner of the screen in 3d world space
    /// </summary>
    public static Vector3 m_topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
    /// <summary>
    /// Bottom left corner of the screen in 3d world space
    /// </summary>
    public static Vector3 m_bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
    /// <summary>
    /// Bottom right corner of the screen in 3d world space
    /// </summary>
    public static Vector3 m_bottomRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0));
}
