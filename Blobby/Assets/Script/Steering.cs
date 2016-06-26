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
    public Spawn spawn;
    public Colors color = Colors.UNKN;
    public float radius;

    public BlobSim BS;

    public Material blackCubeMat;

    // Use this for initialization
    void Start () {
       // radius = GetComponent<SphereCollider>().radius;
       // m = transform.localScale.magnitude;
        //originalScale = transform.localScale;
        InvokeRepeating("CreateDestination", 0, 2);
        area = transform.parent.gameObject;
        spawn = area.GetComponent<Spawn>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        if(isWandering) Wander();
       // Grow();
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
    /*
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
        if(transform.localScale.magnitude >  1.25f * m)
        {
            Split();
        }
    } */

    public void ComboColors( Steering other )
    {
        Colors colorHit = other.color;

        if( color == Colors.BLACK || colorHit == Colors.BLACK )
        {
            MergeBlob(other, Colors.BLACK);
            return;
        }

        switch(color)
        {
            case Colors.GREEN:
                if(colorHit == Colors.ORANGE)
                {
                    MergeBlob( other, Colors.YELLOW);
                }
                else if(colorHit == Colors.PURPLE)
                {
                    MergeBlob( other, Colors.BLUE);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.BLUE:
                if (colorHit == Colors.RED)
                {
                    MergeBlob( other, Colors.PURPLE);
                }
                else if (colorHit == Colors.YELLOW)
                {
                    MergeBlob( other, Colors.GREEN);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.PURPLE:
                if (colorHit == Colors.ORANGE)
                {
                    MergeBlob( other, Colors.RED);
                }
                else if (colorHit == Colors.GREEN)
                {
                    MergeBlob( other, Colors.BLUE);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.RED:
                if (colorHit == Colors.YELLOW)
                {
                    MergeBlob( other, Colors.ORANGE);
                }
                else if (colorHit == Colors.BLUE)
                {
                    MergeBlob( other, Colors.PURPLE);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.ORANGE:
                if (colorHit == Colors.GREEN)
                {
                    MergeBlob( other, Colors.YELLOW);
                }
                else if (colorHit == Colors.PURPLE)
                {
                    MergeBlob( other, Colors.RED);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.YELLOW:
                if (colorHit == Colors.BLUE)
                {
                    MergeBlob( other, Colors.GREEN);
                }
                else if (colorHit == Colors.RED)
                {
                    MergeBlob( other, Colors.ORANGE);
                }
                else
                {
                    //Debug.Log("Wrong combo");
                }
                break;
        }
    }

    public void setColor( Colors c ) {
        if (c == color) return;
        color = c;
        Color col = Color.black;
        switch(c) {
            case Colors.GREEN:
                col  = GameManager.green;
                break;
            case Colors.BLUE:
                col  = GameManager.blue;
                break;
            case Colors.PURPLE:
                col = GameManager.purple;
                break;
            case Colors.RED:
                col  = GameManager.red;
                break;
            case Colors.ORANGE:
                col  = GameManager.orange;
                break;
            case Colors.YELLOW:
                col = GameManager.yellow;
                break;
            case Colors.BLACK:
                
                BS.MC.gameObject.layer = 1;
                BS.MC.GetComponent<MeshRenderer>().material = blackCubeMat;
                spawn.activeBlob--;
                return;
        }
        GetComponentInChildren<MeshRenderer>().material.color = col;
        BS.MC.GetComponent<MeshRenderer>().material.color = col;
    }

    void MergeBlob( Steering other, Colors newColor)
    {
        //Debug.Log(newColor.ToString());
        setColor(newColor);
        other.setColor(newColor);
    }
    public void die() {
        Destroy(gameObject);
        Destroy(BS.transform.parent.gameObject);
        spawn.spawnedObjects.Remove(this);
        if( color != Colors.BLACK )
            spawn.activeBlob--;
    }
}
