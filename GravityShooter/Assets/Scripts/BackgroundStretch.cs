/// ERIC MOULEDOUX
using UnityEngine;
using System.Collections.Generic;
[RequireComponent (typeof(SpriteRenderer))]

///<summary>
/// Stretches any sprite this script is attached to to fit the screen size
///</summary>
public class BackgroundStretch : MonoBehaviour
{
    void Start ()
    {
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight / Screen.height * Screen.width;

        float imageHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float imageWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        transform.localScale = new Vector3(screenWidth / imageWidth, screenHeight / imageHeight, 1);
    }
}
