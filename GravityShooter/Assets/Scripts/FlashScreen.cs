using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashScreen : MonoBehaviour
{
    public Image damageImage;
    public float flashSpeed = 5.0f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    /// <summary>
    /// When the player gets hit/damage, the screen will flash red.
    /// </summary>

    void Update()
    {
        //FlashScreenColor();
    }

    void FlashScreenColor()
    {
        //If Damaged, flash screen red
        //if (Input.GetKeyDown(KeyCode.D))
        //{
            damageImage.color = flashColor;
        //}

        //else
        //{
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}
    }
}
