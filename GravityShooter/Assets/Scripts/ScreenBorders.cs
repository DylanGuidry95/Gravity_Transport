using UnityEngine;
using System.Collections;

public class ScreenBorders : MonoBehaviour
{
    private Vector3 m_topLeft;
    private Vector3 m_topRight;
    private Vector3 m_bottomLeft;
    private Vector3 m_bottomRight;

    public Vector3 TopRight { get { return m_topRight; } }
    public Vector3 TopLeft { get { return m_topLeft; } }
    public Vector3 BottomRight { get { return m_bottomRight; } }
    public Vector3 BottomLeft { get { return m_bottomLeft; } }

    void Awake()
    {
        Camera camera = Camera.main;

        Vector3 tl = new Vector3(0, camera.pixelHeight, 0);
        Vector3 tr = new Vector3(camera.pixelWidth, camera.pixelHeight, 0);
        Vector3 bl = new Vector3(0, 0, 0);
        Vector3 br = new Vector3(camera.pixelWidth, 0, 0);

        m_topLeft = camera.ScreenToWorldPoint(tl);
        m_topRight = camera.ScreenToWorldPoint(tr);
        m_bottomLeft = camera.ScreenToWorldPoint(bl);
        m_bottomRight = camera.ScreenToWorldPoint(br);

        m_topLeft.z = m_topRight.z = m_bottomLeft.z = m_bottomRight.z = 0;
    }
}
