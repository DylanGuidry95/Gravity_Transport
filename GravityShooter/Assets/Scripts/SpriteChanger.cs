using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteChanger : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] sprites;
    int i = 0;
    /// <summary>
    /// Objective - Change sprites when mouse is highlighted/hovered on 
    /// button at runtime.
    /// </summary>
    public void ChangeSprites()
    {
        ++i;
        imageRenderer.sprite = sprites[i];
    }

    public void OnMouseOver()
    {
        ChangeSprites();
    }
}
