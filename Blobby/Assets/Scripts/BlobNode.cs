using UnityEngine;
using System.Collections;

public class BlobNode : MonoBehaviour {

    public Vector2 InterBlob, OInterBlob ;

    public Vector2 Wander, Follow;
    public float Speed = 5, Acc = 0.01f, Drag = 0.95f, InterMod = 1, FollowStr = 0.1f;

    void Start() {

        Wander = Random.insideUnitCircle*0.2f;
        
    }

    void FixedUpdate() {
        OInterBlob = Vector2.Lerp(InterBlob, OInterBlob, 0.2f);
        Wander += OInterBlob* InterMod;
        InterBlob = Vector2.zero;
       // Wander += Follow * FollowStr;
        Wander *= Drag;
        

        var dir = Random.insideUnitCircle;
        Wander += dir*Acc *Random.Range(0.7f,1.0f) / (dir.magnitude +0.00001f);

        var move = Wander + Follow * FollowStr;
        //move += InterBlob;

        transform.position = (Vector2)transform.position + move *Speed*Time.deltaTime;
    }
}
