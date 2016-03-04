using UnityEngine;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [System.Serializable]
    public class Entity
    {
        GameObject entity;
        float spawnRate;
        bool canSpawn;
    }

    public List<Entity> abc = new List<Entity>();
}