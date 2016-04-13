using UnityEngine;
using System.Collections;
/*
    This singleton class is meant to serve the purpose that there is only one of 
    a certain object that inherits from this class
*/
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.GetComponent<GameObject>());
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            /*
                checks to see if there is another object of this type and if there isn't
                it will not destroy the object when a scene is loaded
            */
            _instance = this.gameObject.GetComponent<T>();
            DontDestroyOnLoad(_instance.GetComponent<GameObject>());

        }

        else
        {
            if (this != _instance)
            {
                /*
                    If the same object does exist it will destroy the current instance of that object
                */
                print("found another instance of ID: " + gameObject.GetInstanceID() + ". Destroying.");
                Destroy(this.gameObject);
            }
        }
    }
}