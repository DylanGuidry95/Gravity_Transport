using UnityEngine;
using System.Collections.Generic;

public class DylanGamePlay : Singleton<DylanGamePlay> {

    protected override void Awake()
    {
        base.Awake();
    }

    static GameObject PauseMenu;
    static GameObject BossHealth;
    static GameObject PlayerInfo;

    // Use this for initialization
    void Start ()
    {
        for(int i = 0; i <= transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "PauseMenu")
            {
                PauseMenu = transform.GetChild(i).gameObject;
            }
            if (transform.GetChild(i).name == "BossDisplay")
            {
                BossHealth = transform.GetChild(i).gameObject;
            }
            if (transform.GetChild(i).name == "PlayerDisplay")
            {
                PlayerInfo = transform.GetChild(i).gameObject;
            }
            if (PauseMenu != null && BossHealth != null && PlayerInfo != null)
                break;
        }
        PauseMenu.gameObject.SetActive(false);
        BossHealth.gameObject.SetActive(false);
    }
	
    public static void TogglePauseMenu(bool state)
    {
        PauseMenu.SetActive(state);
        foreach (UnityEngine.UI.Button b in PauseMenu.GetComponentsInChildren<UnityEngine.UI.Button>())
            b.interactable = state;
    }

    public static void ToggleBossHealth(bool state, int v, int mv)
    {
        BossHealth.GetComponentInChildren<UnityEngine.UI.Slider>().maxValue = mv;
        BossHealth.GetComponentInChildren<UnityEngine.UI.Slider>().value = v;
        BossHealth.SetActive(state);
    }

    public static void UpdateBoss(int v)
    {
        BossHealth.GetComponentInChildren<UnityEngine.UI.Slider>().value = v;
    }

    public static void UpdatePlayer(int h, int l)
    {
        if(PlayerInfo != null)
        {
            foreach (UnityEngine.UI.Text t in PlayerInfo.GetComponentsInChildren<UnityEngine.UI.Text>())
            {
                if (t.name == "Health")
                    t.text = "HP: " + h + " / 3";
                if (t.name == "Lives")
                    t.text = "Lives: " + l;
            }
        }

    }

    public void ResumeClick()
    {
        GameStates.ChangeState("Pause");
    }

    public void MainMenuclick()
    {
        GameStates.ChangeState("MainMenu");
    }

    public void ExitClick()
    {
        GameStates.ChangeState("Exit");
    }
}
