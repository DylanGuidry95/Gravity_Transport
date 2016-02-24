using UnityEngine;
using System.Collections.Generic;
using System;
using XInputDotNetPure;

public enum INPUT_DEVICE
{
    MOBILE,
    XBOX,
    PLAYSTATION,
    KEYBOARD,
    CUSTOM,
    MISSING,
    COUNT
}

public class Controller
{
    public GamePadState cState;
    public GamePadState prevState;

    Controller() { }
}

public class InputHandler : Singleton<InputHandler>
{
    public INPUT_DEVICE ActiveInputDevice;

    Dictionary<KeyCode, string>[] Controls;
    List<RuntimePlatform> PlatformsSupported;
    RuntimePlatform ActivePlatform;

    protected override void Awake()
    {
        base.Awake();
        Messenger.AddListener<KeyCode,string, INPUT_DEVICE>("Adding Control", AddControls);
        Messenger.AddListener<RuntimePlatform>("Adding Platform Support", AddPlatformSupport);
    }

    void Start()
    {
        Controls = new Dictionary<KeyCode, string>[Enum.GetNames(typeof(INPUT_DEVICE)).Length];
        PlatformsSupported = new List<RuntimePlatform>();
        ActivePlatform = CheckActivePlatform();
    }

    void AddControls(KeyCode c, string control, INPUT_DEVICE device)
    {
        switch(device)
        {
            case INPUT_DEVICE.KEYBOARD:
                Controls[0].Add(c, control);
                break;
            case INPUT_DEVICE.MOBILE:
                Controls[1].Add(c, control);
                break;
            case INPUT_DEVICE.PLAYSTATION:
                Controls[2].Add(c, control);
                break;
            case INPUT_DEVICE.XBOX:
                Controls[3].Add(c, control);
                break;
        }
    }

    void Update()
    {


        ActiveInputDevice = CheckForInputDevice();
        if(ActiveInputDevice == INPUT_DEVICE.MISSING)
        {
            Debug.LogError("No valid device for input availiable");
        }
        CheckForControllers();
    }

    
    INPUT_DEVICE CheckForInputDevice()
    {
        foreach(KeyCode k in Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(k) && (int)k < (int)KeyCode.JoystickButton0)
            {
                return INPUT_DEVICE.KEYBOARD;
            }
            else if(Input.GetKeyDown(k) && (int)k > (int)KeyCode.JoystickButton0)
            {
                return INPUT_DEVICE.XBOX;
            }
            else
            {
                Debug.LogError("No valid Input Device found.");
            }
        }
        return 0;
    }


    RuntimePlatform CheckActivePlatform()
    {
        foreach (RuntimePlatform rp in PlatformsSupported)
        {
            if (Application.platform == rp)
            {
                return rp;
            }
            else
            {
                Debug.LogError("Platform not supported");
                //Close Application
                break;
            }
        }

        return 0;
    }


    List<Controller> ConnectedControllers = new List<Controller>(); 

    PlayerIndex controllerIndex;

    void CheckForControllers()
    {
        foreach(Controller c in ConnectedControllers)
        {
            if (!c.prevState.IsConnected)
            {
                for (int i = 0; i < Enum.GetNames(typeof(PlayerIndex)).Length; i++)
                {
                    PlayerIndex controller = (PlayerIndex)i;
                    GamePadState controllerState = GamePad.GetState(controller);
                    if (controllerState.IsConnected)
                    {
                        Debug.Log(string.Format("GamePad found {0}", controller));
                        controllerIndex = controller;
                    }
                }
            }
            c.prevState = c.cState;
            c.cState = GamePad.GetState(controllerIndex);
        }

    }

    void AddPlatformSupport(RuntimePlatform p)
    {
        PlatformsSupported.Add(p);
    }
}
