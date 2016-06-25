using UnityEngine;
using System.Collections.Generic;

public class BlobSim : MonoBehaviour {


    public List<BlobNode> Nodes = new List<BlobNode>();


    void Start() {

        foreach(var n in FindObjectsOfType<BlobNode>())
            Nodes.Add(n);
    }
    public Vector2 Mid;
    Vector2 TL, BR;

    float[] Arr;
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
                    m -= 2;
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

    public int Dim = 30;
    void aUpdate() {

        Arr = new float[Dim*Dim];


        for(int ni = Nodes.Count; ni-- >0; ) {
            var n1 = Nodes[ni];
            Vector2 p1 = n1.transform.position;
            var lp = p1 - Mid;

            int d = Dim/2;
            for(int i = -d; i< d; i++)
                for(int j = -d; j< d; j++) {
                    var ap =  new Vector2(i, j);
                    var ds = (ap-lp).magnitude;
                   // if(ds < 10)
                        Arr[d+i + (d+j) *Dim] += 0.25f/ds;
                }
        }
    }


    void OnDrawGizmos() {

        if(Arr == null) return;
        int d = Dim/2;
        for(int i = -d; i< d; i++)
            for(int j = -d; j< d; j++) {

                Gizmos.color = Color.Lerp(Color.red, Color.green, Arr[d+i + (d+j) *Dim]);
                Gizmos.DrawWireSphere(Mid + new Vector2(i, j), 0.5f );


            }
    }


}
