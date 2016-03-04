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

    public List<Entity> m_entities = new List<Entity>();
}

[System.Serializable]
public class Entity
{
    public GameObject entity;
    [Range(0.0f, 1.0f)]
    public float spawnRate;
}