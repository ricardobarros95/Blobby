﻿using UnityEngine;
using System.Collections;

public class GoalColor : MonoBehaviour {

    public Colors color;
    Score score;

    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    public void OnTriggerEnter(Collider other)
    {
       Debug.Log("triggered");
        Debug.Log(other.gameObject.tag);
//        if (other.gameObject.tag == "Blob") //eww
        {
            var mc = other.GetComponent<MarchingCubes>();
            var str = mc.BS.HigherBlob;
            Debug.Log(str.color.ToString());
            if (str.color == color)
            {
                score.ChangeScore(10);
                str.die();
               
            }
            else
            {

                str.Vel = Vector2.Lerp(str.Vel, transform.right * 500, 10 * Time.deltaTime);
            }
        }
    }
}
