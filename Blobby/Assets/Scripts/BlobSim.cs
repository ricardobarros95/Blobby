using UnityEngine;
using System.Collections.Generic;

public class BlobSim : MonoBehaviour {

    public MarchingCubes MC;
    public List<BlobNode> Nodes = new List<BlobNode>();

    bool fix = false;
    void Start() {
        if(!fix) {

           // UnityEngine.Profiler.maxNumberOfSamplesPerFrame *= 3;
            fix = true;
        }
        Nodes.Clear();
        foreach(var n in GetComponentsInChildren<BlobNode>())
            Nodes.Add(n);

        MC = transform.parent.GetComponentInChildren<MarchingCubes>();
    }
    public Vector2 Mid;
    Vector2 TL, BR;

    public float Rad = 2;
    public float Centreing = 0.5f;
    public Steering HigherBlob;

    public float SpreadDrag = 0.75f;
    void FixedUpdate() {

        Vector2 mid = Vector2.zero;

        float cm = 1;
        for(int i = Nodes.Count; i-- >0; ) {
            var n1 = Nodes[i];
            Vector2 p1 = n1.transform.position;
            mid += p1;
            n1.Follow = Vector2.Lerp( n1.Follow, ((Vector2)HigherBlob.transform.position - (Vector2)MC.transform.position  + (HigherBlob.Vel - n1.Follow)*0.5f ), Centreing * Time.deltaTime *cm);
            cm *= SpreadDrag;
            for(int j = i; j-- > 0; ) {
                var n2 = Nodes[j];
                Vector2 p2 = n2.transform.position;
                

                var vec = p2 - p1;
                var m = vec.magnitude;
                
                    if(m < Mathf.Epsilon) {
                        vec = Random.insideUnitCircle*0.01f;
                        m = vec.magnitude;
                    }
    
                    vec /= m;
                    m /= Rad/4;
                    m -= 1;
                    if(m > 0) {
                        m*= 0.5f;
                        m = m *m;
                    } else m = m *m*m;
                   
                    //m -= 0.75f;
                    vec *= m;
                           
             //   vec *= 0.1f;
                    vec /= Nodes.Count;
                n1.InterBlob += vec;
                n2.InterBlob -= vec;

            }

        }
        mid /= Nodes.Count;

        Mid = mid;

    }



   

}
