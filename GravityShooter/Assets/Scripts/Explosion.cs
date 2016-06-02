/// ERIC MOULEDOUX
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Explosion : MonoBehaviour
{
    /// <summary>
    /// Function that will destory the object this script is attached to.
    /// </summary>
    public void Remove()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        GetComponent<AudioSource>().volume = VolumeLevels.Effects;
    }
}
