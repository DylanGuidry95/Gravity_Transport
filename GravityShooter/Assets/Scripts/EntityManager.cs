/// ERIC MOULEDOUX and dylan
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Manages the enemies as waves
/// </summary>
public class EntityManager : MonoBehaviour
{
    void Awake()
    {
        m_currentWave = 0;  // Sets the first wave to 0
        SpawnNextWave();    // Spawns the first wave of entities
    }

    void Update()
    {
        if (Entities.Count > 0 && Reset == false)   // Makes sure that there are still entities alive
        {
            foreach (GameObject e in Entities)      // For each entity that we are managing
            {
                if(e)   // If its still alive
                {
                    if (e.transform.position.x < ScreenBorders.m_bottomLeft.x - 10) // If the entity is off the screen
                    {
                        Destroy(e); // Destroy it
                    }
                }

                if (!e) // Sees if the entity has been destroyed
                {
                    Entities.Remove(e);     // Remove them from the list
                    for (int i = 0; i < Entities.Count; i++)
                    {
                        if (Entities[i].GetComponent<LgEnemy>())
                        {
                            if (i == Entities.Count - 1)
                            {
                                Entities[i].GetComponent<LgEnemy>().CallForHelp();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            return; // and restart the check
                        }
                    }
                    return;

                }
            }
        }

        else if(Reset == true)
        {
            foreach(GameObject e in Entities)
            {
                Destroy(e);
            }
            Reset = false;
            SpawnNextWave();
        }
        else if(++m_currentWave < EntityWaves.Count)    // If all the entites are dead
        {                                               // AND there is a NEXT wave
            SpawnNextWave();                            // spawn the next wave
        }
        else                                            // If there is no next wave
        {

            StartCoroutine(EndWait());

            GameStates.ChangeState("GameOver");         // Gameover

        }
    }

    void SpawnNextWave()
    {
        GameObject wave = // The current wave we are spawning
            Instantiate(EntityWaves[m_currentWave], transform.position, transform.localRotation) as GameObject;

        Entities = new List<GameObject>();                  // Makes sure the old list of entities are clear
        for(int i = 0; i < wave.transform.childCount; ++i)      // and for every new entity the the new wave
        {
            Entities.Add(wave.transform.GetChild(i).gameObject);    // Add them to the new list of entites
        }

        wave.transform.DetachChildren();    // Unparent them from the wave
        Destroy(wave);                      // and destroy the now empty wave
    }

    public static bool Reset = false;

    IEnumerator EndWait()
    {
        yield return new WaitForSeconds(3.0f);
        GameStates.ChangeState("GameOver");
    }

    public static void ResetWave()
    {
        Reset = true;
        foreach(GameObject g in Entities)
        {
            if(g.GetComponent<LgEnemy>())
            {
                foreach (GameObject go in g.GetComponent<LgEnemy>().SmEnemy)
                    Destroy(go);
            }
        }
    }

    /// <summary>
    /// CUrrent wave index
    /// </summary>
    private int m_currentWave;

    /// <summary>
    /// Premade waves of enemies
    /// </summary>
    public List<GameObject> EntityWaves = new List<GameObject>();
    /// <summary>
    /// Enemies in each wave
    /// </summary>
    public static List<GameObject> Entities = new List<GameObject>();
}
