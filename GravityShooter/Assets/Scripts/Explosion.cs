using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public void Remove()
    {
        Destroy(gameObject);
    }
}
