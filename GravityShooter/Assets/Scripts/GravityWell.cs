using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GravityWell : MonoBehaviour
{
    void Update()
    {
        foreach(GravityObject go in m_gravObjects)
        {
            switch(go.state)
            {
                case GRAV.INIT:
                    break;
                case GRAV.ENTER:
                    break;
                case GRAV.THRESHOLD:
                    break;
                case GRAV.BROKEN:
                    break;
                case GRAV.END:
                    break;  
            }; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }

    public float m_speedModifier = 1;

    public int m_gravMax = 1;

    private List<GravityObject> m_gravObjects = new List<GravityObject>();

    public class GravityObject
    {
        public GameObject entity;
        public Vector2 entry;
        public Vector2 thres;
        public Vector2 broke;
        public Vector2 exit;
        public GRAV state;
    }

    public enum GRAV
    {
        INIT,
        ENTER,
        THRESHOLD,
        BROKEN,
        END,
    }
}