using UnityEngine;
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

        Steering.ReflectPId = Shader.PropertyToID("_ReflectColor");
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
            var so1 = spawnedObjects[i];
            var p1 = so1.BS.Mid;

            if(spawnedObjects[i].transform.position.y > 40)
            {
                spawnedObjects[i].Vel -= new Vector2(0, 1);
            }
            if(spawnedObjects[i].transform.position.y < -40)
            {
                spawnedObjects[i].Vel += new Vector2(0, 1);
            }
            if(spawnedObjects[i].transform.position.x > 70)
            {
                Debug.Log("bounce");
                spawnedObjects[i].Vel -= new Vector2(1, 0);
            }
            if(spawnedObjects[i].transform.position.x < -70)
            {
                spawnedObjects[i].Vel += new Vector2(1, 0);
            }

            for (int j = 0; j < i; j++)
            {
                var so2 = spawnedObjects[j];
                var p2 = so2.BS.Mid;
                Vector2 opositeDirection = -((Vector2)so2.transform.position - (Vector2)so1.transform.position  +(so2.Vel - so1.Vel)*0.5f);
                float magnitude = opositeDirection.sqrMagnitude;
                if (magnitude < avoidDistance * avoidDistance)
                {
                    Debug.DrawLine(so2.transform.position, so1.transform.position, Color.white);
                    if(Mathf.Epsilon > magnitude)
                    {
                        continue;
                    }
                    Vector3 savedOpositeDirection = opositeDirection;
                    opositeDirection = opositeDirection / magnitude;
                    
                    
                    if(so1.color == Colors.BLACK && so2.color != Colors.BLACK )
                        so1.AvoidVel -= (Vector2)opositeDirection * avoidanceFactor * 8.5f;
                    else
                        so1.AvoidVel += (Vector2)opositeDirection * avoidanceFactor* so1.AvoidMod;

                    if (so2.color == Colors.BLACK && so1.color != Colors.BLACK)
                        so2.AvoidVel += (Vector2)opositeDirection * avoidanceFactor * 8.5f;
                    else
                        so2.AvoidVel -= (Vector2)opositeDirection * avoidanceFactor * so2.AvoidMod;

                    if( so1.color != so2.color ) // ??
                        if(magnitude < Mathf.Pow((so1.radius + so2.radius)*3, 2))
                        {
                            foreach( var bn1 in so1.BS.Nodes )
                                foreach(var bn2 in so2.BS.Nodes)
                                    if((bn1.transform.position - bn2.transform.position).sqrMagnitude < 9*9) {
                                        so1.ComboColors(so2);
                                        return;
                                    }
                        }
                }

            }
        }
    }
    public int activeBlob = 0;
    int blickChance = 0;
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
        bs.Mid = bs.transform.position;
        bs.HigherBlob = gj.transform.GetComponent<Steering>();
        gj.GetComponent<Steering>().BS = bs;
        bs.MC = b2.GetComponentInChildren<MarchingCubes>();
        gj.GetComponent<Steering>().BlobMR =  bs.MC.GetComponent<MeshRenderer>();
        gj.transform.SetParent(gameObject.transform);
        spawnedObjects.Add(gj.GetComponent<Steering>());
        
        //if( spawnedObjects.Count > 0 )
       // {
           // blickChance = Mathf.Max( activeBlob - (spawnedObjects.Count - activeBlob), 0);
        //}
        int color = Random.Range(0, 10);

        gj.GetComponent<Steering>().setColor(Colors.GREEN);
        gj.GetComponent<Steering>().color = Colors.UNKN;

        gj.GetComponent<Steering>().spawn = this;
        if (blickChance > color && spawnedObjects.Count > 3)
        {
            //   gj.GetComponent<MeshRenderer>().material.color = Color.green;
            gj.GetComponent<Steering>().setColor(Colors.BLACK);
            blickChance = 0;

        }
        else
        {
            blickChance++;
            int colorfull = Random.Range(0, 6);
            if (colorfull == 0)
            {
                //   gj.GetComponent<MeshRenderer>().material.color = Color.green;
                gj.GetComponent<Steering>().setColor(Colors.GREEN);
            }
            else if (colorfull == 1)
            {
                //gj.GetComponent<MeshRenderer>().material.color = Color.red;
                gj.GetComponent<Steering>().setColor(Colors.RED);
            }
            else if (colorfull == 2)
            {
                // gj.GetComponent<MeshRenderer>().material.color = Color.blue;
                gj.GetComponent<Steering>().setColor(Colors.BLUE);
            }
            else if (colorfull == 3)
            {
                //gj.GetComponent<MeshRenderer>().material.color = GameManager.orange;
                gj.GetComponent<Steering>().setColor(Colors.ORANGE);
            }
            else if (colorfull == 4)
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
