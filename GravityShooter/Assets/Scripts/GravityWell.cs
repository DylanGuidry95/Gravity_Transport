using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector3 position = transform.position;
            Vector3 otherPos = other.transform.position;

            Vector3 otherToPosition =  position - otherPos;
            otherToPosition.Normalize();

            rb.velocity += new Vector2(otherToPosition.x, otherToPosition.y) * speedModifier * Time.deltaTime;

            if (position.y > otherPos.y)
                rb.velocity += new Vector2(Time.deltaTime, 0) * speedModifier;
        }
    }

    public float speedModifier = 1;
}