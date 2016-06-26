using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creeps : MonoBehaviour {


    List<Animator> L1 = new List<Animator>(), L2  = new List<Animator>();

    void Start() {
        foreach( var a in GetComponentsInChildren<Animator>() )
            L1.Add(a );

    }
    public int Stage = 1;
    public void step() {
        
        if(L1.Count <= 0) return;
        var a = L1[Random.Range(0, L1.Count)];

        L1.Remove(a);
        L2.Add(a);
        a.SetInteger("CreepLv", Stage);

        Debug.Log(" step  "+Stage);
        if(L1.Count <1) {
            if(Stage++ >= 3) return;
            var tl = L2;
            L2 = L1;
            L1 = tl;
        }
        
    }
}
