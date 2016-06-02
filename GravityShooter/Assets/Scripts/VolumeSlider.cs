using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Slider))]

public class VolumeSlider : MonoBehaviour
{
    public void OnEnable()
    {
        Slider value = GetComponent<Slider>();

        switch (slider)
        {
            case SliderType.MASTER:
                value.value = VolumeLevels.Master;
                break;
            case SliderType.MUSIC:
                value.value = VolumeLevels.Music;
                break;
            case SliderType.EFFECTS:
                value.value = VolumeLevels.Effects;
                break;
        }
    }

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
