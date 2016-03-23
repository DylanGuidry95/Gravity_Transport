using UnityEngine;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    void Awake()
    {
        m_currentWave = 0;
    }

    void Update()
    {
        foreach
    }

    void SpawnNextWave()
    {

    }

    private int m_currentWave;

    public List<GameObject> EntityWaves = new List<GameObject>();
    private List<GameObject> Entities = new List<GameObject>();
}
