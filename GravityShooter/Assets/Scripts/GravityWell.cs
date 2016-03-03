using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GravityWell : MonoBehaviour
{
    void Awake()
    {
        m_GravState = GRAV.INIT;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*
        if (other.gameObject == gravObject)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector3 position = transform.position;
            Vector3 otherPos = other.transform.position;

            Vector3 otherToPosition = position - otherPos;
            otherToPosition.Normalize();

            rb.velocity += new Vector2(otherToPosition.x, otherToPosition.y)
                * (m_speedModifier * Time.deltaTime);

            if (otherPos.x < position.x)
            {
                rb.velocity += new Vector2(Time.deltaTime, 0) * m_speedModifier;
            }
        }

        else if (other.GetComponent<Rigidbody2D>() && gravObject == null)
        {
            gravObject = other.gameObject;
        }
        */
        ///////////////////////////////////////////////////////////////////////////////////////
        if (other.gameObject == gravObject)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector3 position = transform.position;
            Vector3 otherPos = other.transform.position;

            Vector3 otherToPosition = position - otherPos;
            otherToPosition.Normalize();

            Vector2 toGravWell = new Vector2(otherToPosition.x, otherToPosition.y);
            Vector2 right = new Vector2(1, 0);

            switch (m_GravState)
            {
                ////////////////////////////////////////////////////////////////////////
                case GRAV.INIT:
                    m_GravState = GRAV.ENTER;
                    break;
                ////////////////////////////////////////////////////////////////////////
                case GRAV.ENTER:
                    rb.velocity += toGravWell * (m_speedModifier * Time.deltaTime);
                    
                    m_GravState = otherPos.x < position.x ? GRAV.THRESHOLD : m_GravState;
                        break;
                ////////////////////////////////////////////////////////////////////////
                case GRAV.THRESHOLD:
                    rb.velocity += toGravWell * (m_speedModifier * Time.deltaTime);

                    rb.velocity += right * (m_speedModifier * Time.deltaTime);

                    m_GravState = otherPos.x > position.x ? GRAV.BROKEN : m_GravState;
                    break;
                ////////////////////////////////////////////////////////////////////////
                case GRAV.BROKEN:
                    rb.velocity += right * (m_speedModifier * Time.deltaTime);
                    break;
                ////////////////////////////////////////////////////////////////////////
                case GRAV.END:
                    m_GravState = GRAV.END;
                    break;
            };
        }
        else if (other.GetComponent<Rigidbody2D>() && gravObject == null)
        {
            gravObject = other.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == gravObject)
        {
            gravObject = null;
        }
    }

    public float m_speedModifier = 1;

    private GameObject gravObject;
    private GRAV m_GravState;

    private enum GRAV
    {
        INIT,
        ENTER,
        THRESHOLD,
        BROKEN,
        END,
    }
}