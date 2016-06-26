﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {

    public GameObject spawnArea;
    public GameObject prefab, prefab2;
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
                    Debug.DrawLine(spawnedObjects[j].transform.position, spawnedObjects[i].transform.position, Color.white);
                    if(Mathf.Epsilon > magnitude)
                    {
                        continue;
                    }
                    Vector3 savedOpositeDirection = opositeDirection;
                    opositeDirection = opositeDirection / magnitude;
                    
                    if(spawnedObjects[i].color == Colors.BLACK && spawnedObjects[j].color != Colors.BLACK )
                        spawnedObjects[i].Vel -= (Vector2)opositeDirection * avoidanceFactor * 1.5f;
                    else
                        spawnedObjects[i].Vel += (Vector2)opositeDirection * avoidanceFactor;

                    if (spawnedObjects[j].color == Colors.BLACK && spawnedObjects[i].color != Colors.BLACK) 
                        spawnedObjects[j].Vel += (Vector2)opositeDirection * avoidanceFactor * 1.5f;
                    else
                        spawnedObjects[j].Vel -= (Vector2)opositeDirection * avoidanceFactor;


                    if (magnitude < Mathf.Pow(  spawnedObjects[i].radius + spawnedObjects[j].radius, 2 ) )
                    {
                        spawnedObjects[i].ComboColors(spawnedObjects[j]);
                    }
                }

            }
        }
    }
    public int activeBlob = 0;
    private void RandomSpawn()
    {
        activeBlob++;
        Vector3 spawnPosition;
        float xPosition = Random.Range(-spawnArea.transform.lossyScale.x / 2 + 2, spawnArea.transform.lossyScale.x / 2 -2);
        float yPosition = Random.Range(-spawnArea.transform.lossyScale.y / 2 + 2, spawnArea.transform.lossyScale.y / 2 -2);
        spawnPosition = new Vector3(xPosition, yPosition, 0);
        GameObject gj = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
        var b2 = Instantiate(prefab2, spawnPosition, Quaternion.identity) as GameObject;
        var bs = b2.GetComponentInChildren<BlobSim>();
        bs.HigherBlob = gj.transform.GetComponent<Steering>();
        gj.GetComponent<Steering>().BS = bs;
        bs.MC = b2.GetComponentInChildren<MarchingCubes>();
        gj.transform.SetParent(gameObject.transform);
        spawnedObjects.Add(gj.GetComponent<Steering>());
        int blickChance = 0;
        if( spawnedObjects.Count > 0 )
        {
            blickChance = Mathf.Max( activeBlob - (spawnedObjects.Count - activeBlob), 0);
        }
        int color = Random.Range(0, 6*2 + blickChance );
        gj.GetComponent<Steering>().spawn = this;
        if (color >= 6 * 2)
        {
            //   gj.GetComponent<MeshRenderer>().material.color = Color.green;
            gj.GetComponent<Steering>().setColor(Colors.BLACK);
        }
        else
        {
            color %= 6;
            if (color == 0)
            {
                //   gj.GetComponent<MeshRenderer>().material.color = Color.green;
                gj.GetComponent<Steering>().setColor(Colors.GREEN);
            }
            else if (color == 1)
            {
                //gj.GetComponent<MeshRenderer>().material.color = Color.red;
                gj.GetComponent<Steering>().setColor(Colors.RED);
            }
            else if (color == 2)
            {
                // gj.GetComponent<MeshRenderer>().material.color = Color.blue;
                gj.GetComponent<Steering>().setColor(Colors.BLUE);
            }
            else if (color == 3)
            {
                //gj.GetComponent<MeshRenderer>().material.color = GameManager.orange;
                gj.GetComponent<Steering>().setColor(Colors.ORANGE);
            }
            else if (color == 4)
            {
                // gj.GetComponent<MeshRenderer>().material.color = GameManager.purple;
                gj.GetComponent<Steering>().setColor(Colors.PURPLE);
            }
            else
            {
                //gj.GetComponent<MeshRenderer>().material.color = Color.yellow;
                gj.GetComponent<Steering>().setColor(Colors.YELLOW);
            }
        }
    }
}
