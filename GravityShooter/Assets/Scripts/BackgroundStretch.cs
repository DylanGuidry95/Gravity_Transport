using UnityEngine;
using System.Collections;
[RequireComponent (typeof(SpriteRenderer))]

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
