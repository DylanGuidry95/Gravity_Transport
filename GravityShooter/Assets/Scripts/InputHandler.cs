using UnityEngine;
using System.Collections.Generic;

public class InputHandler : Singleton<InputHandler>
{
    Dictionary<KeyCode, string> Controls = new Dictionary<KeyCode, string>();

    protected override void Awake()
    {
        base.Awake();
        Messenger.AddListener<KeyCode,string>("AddControl", AddControls);
    }

    void Start()
    {
        Controls.Add(KeyCode.R, "Shit");
    }

    void AddControls(KeyCode c, string control)
    {
        Controls.Add(c, control);
    }

    void Update()
    {
       foreach(KeyValuePair<KeyCode, string> c in Controls)
        {
            if(Input.GetKeyDown(c.Key))
            {
                Messenger.Broadcast<string>("UserInput", c.Value);
            }
        } 
    }
}
