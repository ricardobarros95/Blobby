using UnityEngine;
using System.Collections.Generic;

public class BlobSim : MonoBehaviour {


    List<BlobNode> Nodes = new List<BlobNode>();


    void Start() {

        foreach(var n in FindObjectsOfType<BlobNode>())
            Nodes.Add(n);
    }

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
                    m -= 3f;
                    vec *= m;
                           
             //   vec *= 0.1f;

                n1.InterBlob += vec;
                n2.InterBlob -= vec;

            }

        }
        mid /= Nodes.Count;


    }

    void OnDrawGizmos() {

   

    }


}
