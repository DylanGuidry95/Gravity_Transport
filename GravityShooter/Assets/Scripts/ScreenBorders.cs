using UnityEngine;
using System.Collections;

public class ScreenBorders
{
    private static Camera camera = Camera.main;

    public static Vector3 m_topLeft = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 0));
    public static Vector3 m_topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
    public static Vector3 m_bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
    public static Vector3 m_bottomRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0));
}
