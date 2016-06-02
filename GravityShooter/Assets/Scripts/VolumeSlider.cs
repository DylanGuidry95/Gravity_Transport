using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Slider))]

public class VolumeSlider : MonoBehaviour
{
    public void Adjust()
    {
        float value = GetComponent<Slider>().normalizedValue;

        switch (slider)
        {
            case SliderType.MASTER:
                VolumeLevels.Master = value;
                break;
            case SliderType.MUSIC:
                VolumeLevels.Music = value;
                break;
            case SliderType.EFFECTS:
                VolumeLevels.Effects = value;
                break;
        }

    }

    public enum SliderType
    {
        MASTER,
        MUSIC,
        EFFECTS,
    }

    public SliderType slider;
}
