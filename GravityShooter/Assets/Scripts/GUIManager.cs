using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : Singleton<GUIManager>
{
    public PlayerGUI playerGUI;
    public BossGUI bossGUI;
    //public ScoreUI scoreGUI;
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
    protected override void Awake()
    {
        base.Awake();
        //create the elements for the dictionary
        m_elements = new Dictionary<string, GameObject>();
        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            m_elements.Add(t.name, t.gameObject);
        }
        PlayerGUI pg = gameObject.GetComponentInChildren<PlayerGUI>();
        BossGUI bg = gameObject.GetComponentInChildren<BossGUI>();
        // RegisterObjects();
    }
    /// <summary>
    /// dictionary of all the elements that the gui will have
    /// turn on and turn off using the key
    /// </summary>
    private Dictionary<string, GameObject> m_elements;
   
    public void RegisterObjects()
    {
        // When the player in game takes damage, 
        // this function will need to update the player's health
        // Also I need to keep in mind to update the Boss's health and also Score
        // it updates by adding it to the list and calling the function again

        List<Transform> Objects = new List<Transform>();
        foreach (Transform child in transform)
        {
            Objects.Add(child);
        }
    }

    /// <summary>
    /// either activate or deactivate a gui element
    /// </summary>
    /// <param name="name">string name or key of the gui element</param>
    /// <param name="state">true is on#false is off</param>
    public void Activate(string name, bool state)
    {   
        m_elements[name].SetActive(state);
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
           // m_elements.Add(name, go);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void ChangeHealth(int num)
    {
        playerGUI.HPChange(num);
    }

    public void ChangeShield(int num)
    {
        playerGUI.ShieldChange(num);
    }



    public void TurnOn(GameObject on)
    {
      //  on.SetActive(true);
    }

    public void TurnOff(GameObject off)
    {
      //  off.SetActive(false);
    }    
}
