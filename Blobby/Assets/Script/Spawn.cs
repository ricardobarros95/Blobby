using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

    public GameObject spawnArea;
    public GameObject prefab;
    public List<Steering> spawnedObjects;
    public float avoidanceFactor = 1;
    public float avoidDistance = 3.25f;
    public float spawnSpeed = 1;

	// Use this for initialization
	void Start () {
        spawnedObjects = new List<Steering>();
        InvokeRepeating("RandomSpawn", 1, spawnSpeed);
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

                    if(magnitude < Mathf.Pow(  spawnedObjects[i].radius + spawnedObjects[j].radius, 2 ) )
                    {
                        spawnedObjects[i].ComboColors(spawnedObjects[j]);
                    }
                }

            }
        }
    }

    private void RandomSpawn()
    {
        Vector3 spawnPosition;
        float xPosition = Random.Range(-spawnArea.transform.lossyScale.x / 2 + 2, spawnArea.transform.lossyScale.x / 2 -2);
        float yPosition = Random.Range(-spawnArea.transform.lossyScale.y / 2 + 2, spawnArea.transform.lossyScale.y / 2 -2);
        spawnPosition = new Vector3(xPosition, yPosition, 0);
        GameObject gj = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
        gj.transform.SetParent(gameObject.transform);
        spawnedObjects.Add(gj.GetComponent<Steering>());
        int color = Random.Range(0, 6);
        if(color == 0)
        {
            gj.GetComponent<MeshRenderer>().material.color = Color.green;
            gj.GetComponent<Steering>().color = Colors.GREEN;
        }
        else if(color == 1)
        {
            gj.GetComponent<MeshRenderer>().material.color = Color.red;
            gj.GetComponent<Steering>().color = Colors.RED;
        }
        else if(color == 2)
        {
            gj.GetComponent<MeshRenderer>().material.color = Color.blue;
            gj.GetComponent<Steering>().color = Colors.BLUE;
        }
        else if(color == 3)
        {
            gj.GetComponent<MeshRenderer>().material.color = GameManager.orange;
            gj.GetComponent<Steering>().color = Colors.ORANGE;
        }
        else if(color == 4)
        {
            gj.GetComponent<MeshRenderer>().material.color = GameManager.purple;
            gj.GetComponent<Steering>().color = Colors.PURPLE;
        }
        else
        {
            gj.GetComponent<MeshRenderer>().material.color = Color.yellow;
            gj.GetComponent<Steering>().color = Colors.YELLOW;
        }
    }
}
