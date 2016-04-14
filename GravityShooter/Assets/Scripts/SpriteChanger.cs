using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteChanger : MonoBehaviour
{
    public Image imageRenderer;
    public Sprite[] sprites;
    int i;

    void OnMouseOver()
    {
        i++;
        imageRenderer.sprite = sprites[i];
        print("Change sprites");
    }

    void OnMouseExit()
    {
        imageRenderer.sprite = sprites[0];
    }

    /// <summary>
    /// Objective - Change sprites when mouse is highlighted/hovered on 
    /// button at runtime.
    /// </summary>
    //public void ChangeSprites()
    //{
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        i++;
    //        if (i >= 6)
    //        {
    //            i = 0;
    //        }
    //        imageRenderer.sprite = sprites[i];
    //    }
    //}
}
