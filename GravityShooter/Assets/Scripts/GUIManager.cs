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

    public void TurnOn(GameObject on)
    {
        on.SetActive(true);
    }

    public void TurnOff(GameObject off)
    {
        off.SetActive(false);
    }

    public void Update()
    {

    }
}
