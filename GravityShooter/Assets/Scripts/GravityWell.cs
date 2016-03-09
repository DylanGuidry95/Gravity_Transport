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
            Transform position = transform;
            Transform otherPos = go.entity.transform;

            Vector3 otherToPosition = position.position - otherPos.position;
            otherToPosition.Normalize();

            Vector2 toGravWell = new Vector2(otherToPosition.x, otherToPosition.y);
            toGravWell.Normalize();

            float sm = m_speedModifier * Time.deltaTime;

            Vector2 right = new Vector2(1, 0);

            switch (go.state)
            {
                /// Initial //////////////////////////////////////////////////////////
                case GRAV.INIT:
                    go.entity.transform.parent = transform;
                    go.state = GRAV.ENTER;                                          
                    break;                                                          
                /// Mass has entered the well ////////////////////////////////////////
                case GRAV.ENTER:                                                    
                    rb.velocity += rb.velocity * sm;
                                                                                    
                    go.state = otherPos.localPosition.x < 0 ? GRAV.THRESHOLD : go.state; 
                    break;                                                          
                /// Mass has reached the well's threshold ////////////////////////////
                case GRAV.THRESHOLD:
                    print("tres");
                    float cSpeed = rb.velocity.magnitude;

                    go.entity.transform.RotateAround(Vector3.forward, sm);
                    rb.velocity = (rb.velocity.normalized + toGravWell.normalized).normalized * (cSpeed);

                    print(rb.velocity);

                    go.state = otherPos.localPosition.x > 0 ? GRAV.BROKEN : go.state;
                    break;                                                          
                /// Mass has broken the well's threshold /////////////////////////////
                case GRAV.BROKEN:
                    print("broken");
                    rb.velocity = right * sm * 2;
                    break;                                                          
                /// There is no mass or it has left the well /////////////////////////
                case GRAV.END:                                                      
                    go.state = GRAV.INIT;
                    go.entity.transform.parent = null;                                           
                    go.remove = true;                                               
                    break;                                                          
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