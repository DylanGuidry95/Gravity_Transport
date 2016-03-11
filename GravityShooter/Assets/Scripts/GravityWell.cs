using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GravityWell : MonoBehaviour
{
    void FixedUpdate()
    {
        foreach(GravityObject g in m_gravObjects)
        {
            if (g.entity)
            {
                float speed = (Time.deltaTime * m_speedModifier);
                switch (g.state)
                {
                    case GRAV.INIT:
                        g.state = GRAV.ENTER;
                        break;
                    case GRAV.ENTER:
                        if (Vector3.Distance(g.entity.transform.localPosition, g.thres) > 0.1f)
                        {
                            g.entity.transform.localPosition += (g.thres - g.entry) * g.velocity.magnitude * speed;
                        }
                        else
                        {
                            g.state = GRAV.THRESHOLD;
                        }
                        break;
                    case GRAV.THRESHOLD:
                        if (Vector3.Distance(g.entity.transform.localPosition, g.brake) > 0.1f)
                        {
                            g.entity.transform.RotateAround(transform.position,
                                Vector3.forward * (g.entry.y / Mathf.Abs(g.entry.y) * g.entry.x / Mathf.Abs(g.entry.x)),
                                g.velocity.magnitude * speed * 100);

                            Debug.DrawLine(g.entity.transform.position, transform.position);
                        }
                        else
                        {
                            g.state = GRAV.BROKEN;
                        }
                        break;
                    case GRAV.BROKEN:
                        g.rb.isKinematic = false;
                        g.rb.velocity = g.velocity * m_speedModifier * -2;
                        g.state = GRAV.END;
                        break;
                    case GRAV.END:
                        m_gravObjects.Remove(g);
                        g.entity.transform.parent = null;
                        return;
                };
            }
            else
            {
                m_gravObjects.Remove(g);
                return;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        for(int i = 0; i < m_gravObjects.Count; ++i)
        {
            if (m_gravObjects[i].entity == other.gameObject)
                return;
        }
  
        if(m_gravObjects.Count < m_gravMax && other.GetComponent<Projectile>())
        {
            GravityObject g = new GravityObject();
            m_gravObjects.Add(g);

            g.rb = other.GetComponent<Rigidbody2D>();
            g.velocity = g.rb.velocity;
            g.rb.velocity = Vector3.zero;
            g.rb.isKinematic = true;

            g.entity = other.gameObject;
            g.entity.transform.parent = transform;
            
            g.entry = g.entity.transform.localPosition;
            g.thres = g.entry + (g.velocity.normalized);
            g.brake = new Vector3(g.thres.x, g.thres.y, 0) * -1;

            g.state = GRAV.INIT;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < m_gravObjects.Count; ++i)
        {
            if (m_gravObjects[i].entity == other.gameObject)
            {
                m_gravObjects.Remove(m_gravObjects[i]);
            }
        }
    }

    public float m_speedModifier = 1;

    public int m_gravMax = 1;

    private List<GravityObject> m_gravObjects = new List<GravityObject>();

    public class GravityObject
    {
        public GameObject entity;
        public Rigidbody2D rb;

        public Vector3 velocity;

        public Vector3 entry;
        public Vector3 thres;
        public Vector3 brake;

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