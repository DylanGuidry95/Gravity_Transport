using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LivesRemaining : MonoBehaviour
{
    public int lives;
    public GameObject ship;
    private static List<GameObject> ships = new List<GameObject>();
    
    void Awake()
    {
        CreateLives(lives);
    }

    /// <summary>
    /// Create 2D sprites for how many lives the user has. 
    /// </summary>
    /// <param name="lives">
    /// Creates how many sprites depending on lives parameter
    /// </param>
    private void CreateLives(int lives)
    {
        for(int i = 0; i < lives; i++)
        {
            float spaceX = (GetComponent<RectTransform>().position.x + 20) + i * 30;
            GameObject remainingLife = Instantiate(ship, new Vector3(spaceX, 0,0), Quaternion.identity) as GameObject;
            remainingLife.transform.parent = gameObject.transform;
            remainingLife.transform.localScale += new Vector3(1, 1, 1);
            ships.Add(remainingLife);
        }
    }

    /// <summary>
    /// Removing/Destroying sprite from list and sprite.
    /// </summary>
    public static void RemoveLife()
    {
        if(ships.Count - 1 >= 0)
        {
            Destroy(ships[ships.Count - 1].gameObject);
            ships.Remove(ships[ships.Count - 1]);
        }
    }
}
