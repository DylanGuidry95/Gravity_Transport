using UnityEngine;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    void Awake()
    {
        Camera camera = Camera.main;

        Vector3 tl = new Vector3(0,                 camera.pixelHeight, 0);
        Vector3 tr = new Vector3(camera.pixelWidth, camera.pixelHeight, 0);
        Vector3 bl = new Vector3(0,                 0,                  0);
        Vector3 br = new Vector3(camera.pixelWidth, 0,                  0);

        m_topLeft       = camera.ScreenToWorldPoint(tl);
        m_topRight      = camera.ScreenToWorldPoint(tr);
        m_bottomLeft    = camera.ScreenToWorldPoint(bl);
        m_bottomRight   = camera.ScreenToWorldPoint(br);

        m_topLeft.z = m_topRight.z = m_bottomLeft.z = m_bottomRight.z = 0;
    }
    	
	// Update is called once per frame
	void Update ()
    {
        foreach (Entity e in m_entities)
        {
            e.spawnTimer += Time.deltaTime;

            if (e.spawnTimer * e.spawnRate > m_spawnDelay)
            {
                Vector3 spawnPoint =
                    new Vector3(Random.Range(m_topRight.x, m_topRight.x + 1.0f),
                                Random.Range(m_bottomRight.y + 1.0f, m_topRight.y - 1.0f),
                                0);

                GameObject go = Instantiate(e.entity, spawnPoint, e.entity.transform.localRotation) as GameObject;

                m_spawnedObjects.Add(go);

                e.spawnTimer = 0;
            }
        }

        foreach(GameObject go in m_spawnedObjects)
        {
            if (go)
            {
                if (go.transform.position.x < m_bottomLeft.x - 1)
                {
                    m_spawnedObjects.Remove(go);
                    Destroy(go);
                    return;
                }
            }
        }
	}

    public float m_spawnDelay = 1;

    Vector3 m_topLeft;
    Vector3 m_topRight;
    Vector3 m_bottomLeft;
    Vector3 m_bottomRight;

    public List<Entity> m_entities = new List<Entity>();

    List<GameObject> m_spawnedObjects = new List<GameObject>();
}

[System.Serializable]
public class Entity
{
    public GameObject entity;
    [Range(0.0f, 1.0f)]
    public float spawnRate = 1.0f;
    [HideInInspector]
    public float spawnTimer = 0.0f;
}