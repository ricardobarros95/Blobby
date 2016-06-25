using UnityEngine;
using System.Collections;

struct ciliderVolume
{
    public float height;
    public float radius;
}
public class Steering : MonoBehaviour {

    const float zPosition = -2;
    private GameObject area;
    private Vector3 destination;
    private float distance;
    public float speed;
    ciliderVolume volume;
    bool isWandering = false;
    float acceleration;
    public float growFactor = 0.0001f;
    private bool isSplitting = false;
    float m;
    public GameObject prefab;
    Vector3 originalScale;
    Spawn spawn;
    // Use this for initialization
    void Start () {

        m = transform.localScale.magnitude;
        originalScale = transform.localScale;
        area = transform.parent.gameObject;
        spawn = area.GetComponent<Spawn>();
        InvokeRepeating("CreateDestination", 0, 2);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(isWandering) Wander();
        Grow();
    }
    public Vector2 Vel;
    void Wander()
    {
        float distanceTravelled = Time.deltaTime * speed;
        float currentTravel = distanceTravelled / distance;
        //gameObject.transform.position = Vector3.Lerp(transform.position, destination, currentTravel);

        Vel *= 0.95f;
        Vel += ((Vector2)destination - (Vector2)transform.position).normalized;
        transform.position += (Vector3)Vel * Time.deltaTime * speed;
    }

    void CreateDestination()
    {
        float xPosition = Random.Range(-area.transform.lossyScale.x / 2, area.transform.lossyScale.x / 2);
        float yPosition = Random.Range(-area.transform.lossyScale.y / 2, area.transform.lossyScale.y / 2);
        destination = new Vector3(xPosition, yPosition, zPosition);
        distance = Vector3.Distance(transform.position, destination);
        isWandering = true;
    }

    void Split()
    {
        transform.localScale = originalScale;
        GameObject gj = Instantiate(prefab, transform.position + transform.position /10, Quaternion.identity) as GameObject;
        spawn.spawnedObjects.Add(gj.GetComponent<Steering>());
        gj.transform.SetParent(transform.parent);
        gj.transform.localScale = originalScale;
        isSplitting = false;
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growFactor;
        if(transform.localScale.magnitude >  1.05f * m)
        {
            Split();
        }
    }

}
