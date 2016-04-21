using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A rail system for objects in a "GravityWell"
/// </summary>
public class GravityWell : Singleton<GravityWell>
{
    void FixedUpdate()
    {
        foreach(GravityObject g in m_gravObjects)
        {
            if (g.entity)
            {
                float speed = (Time.deltaTime * m_speedModifier);
                float toWell = Vector3.Distance(g.entity.transform.position, gameObject.transform.position);

                switch (g.state)
                {
                    case GRAV.INIT:
                        gameObject.GetComponent<AudioSource>().pitch += 0.25f;
                        g.entity.GetComponent<Projectile>().isEnemy = false;
                        g.state = GRAV.ENTER;
                        break;

                    case GRAV.ENTER:
                        float toThres = Vector3.Distance(g.entity.transform.localPosition, g.thres);
                        if (toThres > 0.1f)
                        {
                            g.entity.transform.localPosition +=
                                (g.thres - g.entry) * g.velocity.magnitude * speed;
                        }
                        else
                        {
                            g.state = GRAV.THRESHOLD;
                        }
                        break;

                    case GRAV.THRESHOLD:
                        float toBreak = Vector3.Distance(g.entity.transform.localPosition, g.brake);
                        if (toBreak > 0.1f)
                        {
                            g.entity.transform.RotateAround(transform.position, Vector3.forward *

                                (g.thres.y < 0 ? -1 : 1),

                                g.velocity.magnitude * speed * 100/toWell);
                        }
                        else
                        {
                            g.state = GRAV.BROKEN;
                        }
                        break;

                    case GRAV.BROKEN:
                        g.rb.isKinematic = false;
                        g.rb.velocity = -g.velocity * m_speedModifier * 2;
                        g.state = GRAV.END;
                        break;

                    case GRAV.END:
                        m_gravObjects.Remove(g);
                        g.entity.transform.parent = null;
                        gameObject.GetComponent<AudioSource>().pitch -= 0.25f;
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
        for(int i = 0; i < m_gravObjects.Count; ++i)            // Checks every GravityObject in the GravityWell 
        {
            if (m_gravObjects[i].entity == other.gameObject)    // If the object is already in the list
                return;                                             // Do nothing
        }
  
        if(m_gravObjects.Count < m_gravMax &&                   // If the current number of affected objects is less than the max
            other.GetComponent<Projectile>())                   // And the object is something that can be affected by the GravityWell - Any object with the "Projectile.cs" script on it
        {
            GravityObject g = new GravityObject();              // Make a new GravityObject

            g.entity = other.gameObject;                        // Saves the GameObject of the object
            g.entity.transform.parent = transform;              // Set its parent to the GravityWell
                                                                    // So it will move reletive to the GravityWell

            g.rb = other.GetComponent<Rigidbody2D>() ?          // Makes sure the GravityObject has a RigidBody
                other.GetComponent<Rigidbody2D>() : null;

            if (g.rb == null) return;                           // If it does not have a RigidBody, do nothing and cancel

                                                                // If the object does have RigidBody
            g.velocity = g.rb.velocity;                         // Save its velocity
            g.rb.velocity = Vector3.zero;                       // Set its velocity to 0
            g.rb.isKinematic = true;                            // And make Kinematic
                                                                    // Makes the object move with Transform and NOT RigidBody
            
            g.entry = g.entity.transform.localPosition;         // Sets the point of entry of the object relative to the GravityWell
            g.thres = g.entry + (g.velocity.normalized);        // Sets the threshold point to the forward direction of entry point to the object
            g.brake = g.thres * -1;                             // Sets the break pont to the threshold point reflected over the GravityWell

            g.state = GRAV.INIT;                                // Sets the current state of the object to "Initial"
                                                                    // Used for poprerly moving the object in a "simulated gravity" fashion

            m_gravObjects.Add(g);                               // Add it to the list of GravityObjects
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < m_gravObjects.Count; ++i)               // Checks every GravityObject in the GravityWell
        {
            if (m_gravObjects[i].entity == other.gameObject &&      // If it's the object that has left the GravityWell
                m_gravObjects[i].state == GRAV.END)                 // And it was supposed to leave the GravityWell
            {
                m_gravObjects[i].entity.transform.parent = null;    // Unparent it from the GravityWell
                m_gravObjects.Remove(m_gravObjects[i]);             // Remove it from the list of affected GravityObjects
            }
        }
    }

    /// <summary>
    /// A modifier to increase or decrease the speed of a GravityObject in the GravityWell
    /// </summary>
    public float m_speedModifier = 1;

    /// <summary>
    /// The maxium number of GravityObjects the GravityWell can influence at once
    /// </summary>
    public int m_gravMax = 1;

    /// <summary>
    /// A list of all the GravityObject currently being affected by the GravityWell
    /// </summary>
    private List<GravityObject> m_gravObjects = new List<GravityObject>();

    /// <summary>
    /// A class to store all relevant information about the Object in the GravityWell
    /// </summary>
    public class GravityObject
    {
        public GameObject entity;   // The object that has entered the GravityWell
        public Rigidbody2D rb;      // The RigidBody attached to the entity
        public Vector3 velocity;    // The original velocity of the entity

        public Vector3 entry;       // Relative point of entry of the entity to the GravityWell
        public Vector3 thres;       // The half-way point of the entitys path to the GravityWell
        public Vector3 brake;       // The point at which the entity is no longer affected by the GravityWell

        public GRAV state;          // The current state of the GravityObject based on its relative position to the GravityWell
    }

    /// <summary>
    /// Possible states based on the relative position of the GravityObject to the GravityWell
    /// </summary>
    public enum GRAV
    {
        INIT,       // the initial state of all GravityObjects
        ENTER,      // the GravityObject has entered the GravityWell
        THRESHOLD,  // the GravityObject has made it half-way through the well
        BROKEN,     // the GravityObject has completed at least 1 semi-rotation around the well
        END,        // the GravityObject has left the GravityWell
    }
} 