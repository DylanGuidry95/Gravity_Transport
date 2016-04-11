using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : Singleton<GUIManager>
{
    /// <summary>
    /// Singleton = restricts the Instantiation of a class to one object
    /// Static classes are lazy-loaded when they are first referenced, 
    /// but must have an empty static constructor (or one is generated for you). 
    /// This means it's easier to mess up and break code if you're not careful and know what you're doing. 
    /// As for using the Singleton Pattern, you automatically already do lots of neat stuff, such as creating them with a static initialization method and making them immutable.
    ///(2) Singleton can implement an interface (Static cannot). 
    /// This allows you to build contracts that you can use for other Singleton objects or just any other class you want to throw around.
    /// In other words, you can have a game object with other components on it for better organization!
    ///(3) You can also inherit from base classes, which you can't do with Static classes.
    ///P.S.: Unfortunately there is no good way to remove the need of a "Instance keyword" right there, calling the singleton.
    ///P.S.(2): This is made as MonoBehaviour because we need Coroutines.A lot of times it makes sense to leave one in a singleton, so it will persist between scenes.
    /// 
    /// Must use instance to access information
    /// Static - they're public but can only be created once
    /// </summary>

    /// <summary>
    /// dictionary of all the elements that the gui will have
    /// turn on and turn off using the key
    /// </summary>

    private Dictionary<string, GameObject> m_elements;

    protected override void Awake()
    {
        // base keeps the orignal function, if you want to change a function, you override it.
        base.Awake();
        
        //create the elements for the dictionary
        m_elements = new Dictionary<string, GameObject>();

        // check parent's children for GUI elements
        foreach(Transform t in transform.parent.GetComponentInChildren<Transform>())
        {
            m_elements.Add(t.name, t.gameObject);
        }
    }

    /// <summary>
    /// either activate or deactivate a gui element
    /// </summary>
    /// <param name="name">string name or key of the gui element</param>
    /// <param name="state">true is on/false is off</param>
    public void Activate(string name, bool state)
    {
        if (m_elements.ContainsKey(name))
        {
            m_elements[name].SetActive(state);
        }
        else
            Debug.Log("No element with name: " + name + " was registered with the GUIManager, but you're trying to activate it.");
    }

    /// <summary>
    /// let gui elements add themselves to this guimanager
    /// </summary>
    /// <param name="name">the string name of the go</param>
    /// <param name="go">the actual go</param>
    /// <returns></returns>
    public bool Register(string name, GameObject go)
    {
        try
        {
            return true;
        }
        catch
        {
            return false;
        }
    }
}

/*
GUIManager.instance.Activate("UIScore", true);

int health = 3;
    public PlayerGUI playerGUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            health--;
            if (health <= 0)
            {
                health = 0;
                GUIManager.instance.Activate("UIPlayer", false);
                LevelLoader.LoadLevel("GameOver");
            }
            else
                playerGUI.HPChange(health);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GUIMenuManager.PauseButton();
        }
    }
*/
