using UnityEngine;
using System.Collections;

public class BlobNode : MonoBehaviour {

    public Vector2 InterBlob;

    public Vector2 Wander;
    public float Speed = 5, Acc = 0.01f, Drag = 0.95f, InterMod = 1;

    void Start() {

        Wander = Random.insideUnitCircle*0.2f;
        
    }

    void FixedUpdate() {

        Wander += InterBlob * InterMod;
        InterBlob = Vector2.zero;
        Wander *= Drag;

        var dir = Random.insideUnitCircle;
        Wander += dir*Acc *Random.Range(0.7f,1.0f) / (dir.magnitude +0.00001f);

        var move = Wander;
        //move += InterBlob;

        transform.position = (Vector2)transform.position + move *Speed*Time.deltaTime;
    }
}
