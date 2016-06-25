using UnityEngine;
using System.Collections.Generic;

public class BlobSim : MonoBehaviour {


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
    }
    public Vector2 Mid;
    Vector2 TL, BR;

    public float Rad = 2;

    void FixedUpdate() {

        Vector2 mid = Vector2.zero;

        for(int i = Nodes.Count; i-- >0; ) {
            var n1 = Nodes[i];
            Vector2 p1 = n1.transform.position;
            mid += p1;

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
