/// ERIC MOULEDOUX
using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    /// <summary>
    /// Function that will destory the object this script is attached to.
    /// </summary>
    public void Remove()
    {
        Destroy(gameObject);
    }
}
