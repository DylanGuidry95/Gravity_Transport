using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlOptions : MonoBehaviour
{
    public Toggle TouchMove;
    public Toggle DirectionalMove;

    public GameObject Volume;

    public enum CONTROLTYPE
    {
        touchMove,
        directionalMove
    }

    public CONTROLTYPE controlConfig;

    void Awake()
    {
        if(!Application.isMobilePlatform)
        {
            this.gameObject.SetActive(false);
            Volume.GetComponent<RectTransform>().localPosition = new Vector3(0, Volume.GetComponent<RectTransform>().localPosition.y, 0);
        }

        else if(Application.isMobilePlatform)
        {
            if (PlayerPrefs.GetInt("ControlConfig") == 0)
            {
                TouchMove.isOn = true;
                controlConfig = CONTROLTYPE.touchMove;
            }
            else
            {
                DirectionalMove.isOn = true;
                controlConfig = CONTROLTYPE.directionalMove;
                ChangeControls(TouchMove);
            }
        }
    }

    public void ChangeControls(Toggle option)
    {
        if(option == TouchMove)
            DirectionalMove.isOn = !TouchMove.isOn;
        else if(option == DirectionalMove)
            TouchMove.isOn = !DirectionalMove.isOn;

        if (TouchMove.isOn)
            controlConfig = CONTROLTYPE.touchMove;
        else
            controlConfig = CONTROLTYPE.directionalMove;

        PlayerPrefs.SetInt("ControlConfig", (int)controlConfig);
    }
}
