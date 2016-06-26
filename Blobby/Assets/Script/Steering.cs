using UnityEngine;
using System.Collections;

struct ciliderVolume {
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
    public float AvoidMod = 1;
    public BlobSim BS;

    public Material blackCubeMat;

    public MeshRenderer BlobMR;
    void Awake() {
        //GetComponent<MeshRenderer>();
    }
    void Start() {
        // radius = GetComponent<SphereCollider>().radius;
        // m = transform.localScale.magnitude;
        //originalScale = transform.localScale;
        InvokeRepeating("CreateDestination", 0, 2);
        area = transform.parent.gameObject;
        spawn = area.GetComponent<Spawn>();

        //  ReflectPId = Shader.PropertyToID("_ReflectColor");
    }
    public float ColorRestRt = 0.5f;
    void Update() {
        if(color == Colors.BLACK) {
            BlobMR.material.SetColor(ReflectPId,
                Color.Lerp(BlobMR.material.GetColor(ReflectPId), blackCubeMat.GetColor(ReflectPId), 5*Time.deltaTime));
        }
        float cur = 1, high =0.001f;
        int highI = -1;

        float tot = 0;
        for(int i = ColChng.Length; i-- >0; ) {

            if(i == (int)color) {

            } else {
                if(ColChng[i] > 0.9f)
                    ColChng[i] = 0.9f;
                if(ColChng[i] > high) {

                    high = ColChng[i];
                    highI = i;
                }
                tot += ColChng[i];
                ColChng[i] -= ColorRestRt * Time.deltaTime;
                if(ColChng[i] < 0)
                    ColChng[i] = 0;
            }
        }

        if(highI != -1) {
            cur = 1- high;
            tot += cur;
            high /= tot;
            cur /= tot;
            ColChng[(int)color] = cur;

            blendColor(highI, high / (high + cur));
        }

        AvoidMod = cur;
    }

    void blendColor( int ci, float m2 ) {
        float m1 = 1-m2;
        var c1 = getColor(color);
        var c2 = getColor((Colors)ci);

        float thresh = 0.6f;
        if((Colors)ci == Colors.BLACK) thresh = 0.8f;
        if(m2 >thresh) {
            setColor((Colors)ci);
            if(color == Colors.BLACK) return;
        }

        var col = Color.Lerp(c1, c2, m2);
        BlobMR.material.SetColor(ReflectPId, col);
    }
    Color getColor( Colors c ) {
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
        }
        return col;
    }
    void FixedUpdate() {

        if(isWandering) Wander();
        // Grow();
    }
    public Vector2 Vel, AvoidVel;

    public float MaxAvoid = 200;
    void Wander() {
        float distanceTravelled = Time.deltaTime * speed;
        float currentTravel = distanceTravelled / distance;
        //gameObject.transform.position = Vector3.Lerp(transform.position, destination, currentTravel);

        Vel *= 0.95f;
        Vel += ((Vector2)destination - (Vector2)transform.position).normalized;

        AvoidVel *= 0.97f;
        var mv = AvoidVel + Vel;
        

        var vm = mv.sqrMagnitude;
        if(vm > MaxAvoid * MaxAvoid ) {
            if(AvoidVel.sqrMagnitude > MaxAvoid *MaxAvoid) {
                Vel = Vector2.zero;
                AvoidVel= AvoidVel.normalized * MaxAvoid;
            } else {
                Vel *= (Mathf.Sqrt(vm - Vector2.Dot(AvoidVel, Vel)*2 - AvoidVel.sqrMagnitude ))/ Vel.magnitude;
                mv = AvoidVel + Vel;
            }
        }
        transform.position += (Vector3)mv * Time.deltaTime * speed;

    }

    void CreateDestination() {
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

    public void ComboColors( Steering other ) {
        Colors colorHit = other.color;



        if(color == Colors.BLACK || colorHit == Colors.BLACK) {
            MergeBlob(other, Colors.BLACK);
            return;
        }

        switch(color) {
            case Colors.GREEN:
                if(colorHit == Colors.ORANGE) {
                    MergeBlob(other, Colors.YELLOW);
                } else if(colorHit == Colors.PURPLE) {
                    MergeBlob(other, Colors.BLUE);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.BLUE:
                if(colorHit == Colors.RED) {
                    MergeBlob(other, Colors.PURPLE);
                } else if(colorHit == Colors.YELLOW) {
                    MergeBlob(other, Colors.GREEN);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.PURPLE:
                if(colorHit == Colors.ORANGE) {
                    MergeBlob(other, Colors.RED);
                } else if(colorHit == Colors.GREEN) {
                    MergeBlob(other, Colors.BLUE);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.RED:
                if(colorHit == Colors.YELLOW) {
                    MergeBlob(other, Colors.ORANGE);
                } else if(colorHit == Colors.BLUE) {
                    MergeBlob(other, Colors.PURPLE);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.ORANGE:
                if(colorHit == Colors.GREEN) {
                    MergeBlob(other, Colors.YELLOW);
                } else if(colorHit == Colors.PURPLE) {
                    MergeBlob(other, Colors.RED);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
            case Colors.YELLOW:
                if(colorHit == Colors.BLUE) {
                    MergeBlob(other, Colors.GREEN);
                } else if(colorHit == Colors.RED) {
                    MergeBlob(other, Colors.ORANGE);
                } else {
                    //Debug.Log("Wrong combo");
                }
                break;
        }
    }

    public void setColor( Colors c ) {
        if(c == color) return;
        if(color == Colors.UNKN) {
            for(int i = ColChng.Length; i-- >0; ) ColChng[i] = 0;
            ColChng[(int)c] = 1;
        } else
            ColChng[(int)c] += 0.2f;
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
                BlobMR.material = blackCubeMat;
                // BlobMR.material.SetColor(ReflectPId, blackCubeMat.GetColor( ReflectPId) );
                spawn.activeBlob--;
                return;
        }
        var mr = GetComponentInChildren<MeshRenderer>();
        if(mr != null) //for debugery
            mr.material.color = col;

        BlobMR.material.SetColor(ReflectPId, col); ;
    }
    static public int ReflectPId = 0;

    public float[] ColChng = new float[7];
    public float ColCnhgSpd = 1;
    void addCol( Colors c ) {
        if(color == Colors.BLACK) return;
        if(c != color) {
            float r = Time.deltaTime *ColCnhgSpd;
            if(c != Colors.BLACK) r *= 1.5f;
            ColChng[(int)c] += r;
        } else {
            for(int i = ColChng.Length; i-- >0; ) ColChng[i] -= Time.deltaTime *ColCnhgSpd*0.5f;
        }
    }
    void MergeBlob( Steering other, Colors newColor ) {
        //Debug.Log(newColor.ToString());

        addCol(newColor);
        other.addCol(newColor);
        if(spawn.CountBlackBobbles() == spawn.maxBubbles) {
            spawn.Lose();
        }

    }
    public void die() {
        Destroy(gameObject);
        Destroy(BS.transform.parent.gameObject);
        spawn.spawnedObjects.Remove(this);
        if(color != Colors.BLACK)
            spawn.activeBlob--;
    }

}
