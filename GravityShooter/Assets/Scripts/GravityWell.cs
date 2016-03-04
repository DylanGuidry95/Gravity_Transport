using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GravityWell : MonoBehaviour
{
    void Update()
    {
        foreach (GravityObject go in m_gravObjects)
        {
            Rigidbody2D rb = go.entity.GetComponent<Rigidbody2D>();
            Vector3 position = transform.position;
            Vector3 otherPos = go.entity.transform.position;

            Vector3 otherToPosition = position - otherPos;
            otherToPosition.Normalize();

            Vector2 toGravWell = new Vector2(otherToPosition.x, otherToPosition.y);
            toGravWell.Normalize();

            Vector2 right = new Vector2(1, 0);

            switch (go.state)
            {
                /// Initial //////////////////////////////////////////////////////////
                case GRAV.INIT:                                                     //
                    go.state = GRAV.ENTER;                                          //
                    break;                                                          //
                /// Mass has entered the well ////////////////////////////////////////
                case GRAV.ENTER:                                                    //
                    rb.velocity += toGravWell * (m_speedModifier * Time.deltaTime);                     //
                                                                                    //
                    go.state = otherPos.x < position.x ? GRAV.THRESHOLD : go.state; //
                    break;                                                          //
                /// Mass has reached the well's threshold ////////////////////////////
                case GRAV.THRESHOLD:                                                //
                    rb.velocity += toGravWell * (m_speedModifier * Time.deltaTime); //
                                                                                    //
                    go.state = otherPos.x > position.x ? GRAV.BROKEN : go.state;    //
                    break;                                                          //
                /// Mass has broken the well's threshold /////////////////////////////
                case GRAV.BROKEN:                                                   //
                    rb.velocity += right * (m_speedModifier * Time.deltaTime) * 2;  //
                    break;                                                          //
                /// There is no mass or it has left the well /////////////////////////
                case GRAV.END:                                                      //
                    go.state = GRAV.INIT;                                           //
                    go.remove = true;                                               //
                    break;                                                          //
                /// End //////////////////////////////////////////////////////////////
            };

            if (go.remove)
            {
                m_gravObjects.Remove(go);
                return;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(m_gravObjects.Count < m_gravMax)
        {
            if(other.GetComponent<Projectile>())
            {
                GravityObject go = new GravityObject();
                go.entity = other.gameObject;
                go.state = GRAV.INIT;
                m_gravObjects.Add(go);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        foreach (GravityObject go in m_gravObjects)
        {
            if (go.entity == other.gameObject)
            {
                go.state = GRAV.END;
            }
        }
    }

    public float m_speedModifier = 1;

    public int m_gravMax = 1;

    private List<GravityObject> m_gravObjects = new List<GravityObject>();

    public class GravityObject
    {
        public GameObject entity;
        public GRAV state;
        public bool remove;
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