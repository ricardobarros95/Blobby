using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

    public GameObject spawnArea;
    public GameObject prefab;
    public List<Steering> spawnedObjects;
    public float avoidanceFactor = 1;
    public float avoidDistance = 3.25f;

	// Use this for initialization
	void Start () {
        spawnedObjects = new List<Steering>();
        InvokeRepeating("RandomSpawn", 1, 2);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        //Avoidance Code
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Vector3 opositeDirection = -(spawnedObjects[j].transform.position - spawnedObjects[i].transform.position);
                float magnitude = opositeDirection.sqrMagnitude;
                if (magnitude < avoidDistance * avoidDistance)
                {
                    if(Mathf.Epsilon > magnitude)
                    {
                        continue;
                    }
                    opositeDirection = opositeDirection / magnitude;
                    
                    spawnedObjects[i].Vel += (Vector2)opositeDirection * avoidanceFactor;

                    spawnedObjects[j].Vel -= (Vector2)opositeDirection * avoidanceFactor;
                }
            }
        }
    }

    private void RandomSpawn()
    {
        Vector3 spawnPosition;
        float xPosition = Random.Range(-spawnArea.transform.lossyScale.x / 2, spawnArea.transform.lossyScale.x / 2);
        float yPosition = Random.Range(-spawnArea.transform.lossyScale.y / 2, spawnArea.transform.lossyScale.y / 2);
        spawnPosition = new Vector3(xPosition, yPosition, 0);
        GameObject gj = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
        gj.transform.SetParent(gameObject.transform);
        spawnedObjects.Add(gj.GetComponent<Steering>());
    }
}
