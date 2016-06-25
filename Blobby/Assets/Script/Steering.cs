using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour {

    public GameObject spawnArea;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating("RandomSpawn", 1, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void RandomSpawn()
    {
        Vector3 spawnPosition;
        float xPosition = Random.Range(-spawnArea.transform.lossyScale.x / 2, spawnArea.transform.lossyScale.x/2);
        float yPosition = Random.Range(-spawnArea.transform.lossyScale.y / 2, spawnArea.transform.lossyScale.y/2);
        spawnPosition = new Vector3(xPosition, yPosition, -2);
        GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
