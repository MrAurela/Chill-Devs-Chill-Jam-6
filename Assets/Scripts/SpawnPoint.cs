using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


[System.Serializable]
public class ObjectsCollection
{
    public List<GameObject> objectList;
}

public class SpawnPoint : MonoBehaviour, ISpawnable
{
    public List<ObjectsCollection> collections;
    public int maxObjects = 10;
    public float objectsScale = 0.1f;
    public float objectsMinScale = 0.35f;
    public float biomeNoiseScale = 1f;
    private List<Transform> spawnPoints;

    private void Start()
    {
        spawnPoints = gameObject.GetComponentsInChildren<Transform>().Skip(1).ToList();
        List<Transform> shuffledPoints = new List<Transform>();
        int[] shuffled = new int[spawnPoints.Count];

        for(int i = 0; i < shuffled.Length ; i++)
        {
            UnityEngine.Random.InitState(System.DateTime.Now.Minute+i);
            int randomIndex = UnityEngine.Random.Range(0, shuffled.Length-1);
            int newValue = shuffled[randomIndex];
            int oldValue = shuffled[i];
            shuffled[i] = newValue;
            shuffled[randomIndex] = oldValue;
        }

        foreach(int n in shuffled)
        {
            shuffledPoints.Add(spawnPoints[n]);
            Debug.Log(n);
        }
        spawnPoints = shuffledPoints;
        Spawn();
    }

    public void Spawn()
    {
        int spawnedObj = 0;

        foreach(Transform t in spawnPoints)
        {
            if (spawnedObj >= maxObjects)
                break;
            int seed = DateTime.Now.Minute * 100;
            float noise = Mathf.PerlinNoise(seed+t.position.x* biomeNoiseScale,seed+ t.position.y*biomeNoiseScale);
            int collectionIndex = (int)Mathf.Repeat(Mathf.RoundToInt(noise*10), collections.Count);
            List<GameObject> objects = collections[collectionIndex].objectList;

            int r = UnityEngine.Random.Range(0, objects.Count);
            if(r < objects.Count)
            {
                GameObject newOb = GameObject.Instantiate(objects[r]);
                newOb.transform.parent = t.transform;
                newOb.transform.position = t.position;
                newOb.transform.RotateAround(t.position, Vector3.up, UnityEngine.Random.Range(0, 360));
                float randScale = UnityEngine.Random.Range(objectsMinScale, 1) * objectsScale;
                newOb.transform.localScale = new Vector3(randScale, randScale, randScale);
                spawnedObj++;
            }
        }
    }

    public void Despawn()
    {
        foreach (Transform t in spawnPoints)
        {
            if(t.GetChild(1) != null)
                DestroyImmediate(t.GetChild(1).gameObject);
        }
    }
    public void SpawnUpdate()
    {
        Despawn();
        Spawn();
    }
}
