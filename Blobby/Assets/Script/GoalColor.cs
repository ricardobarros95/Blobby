using UnityEngine;
using System.Collections;

public class GoalColor : MonoBehaviour {

    public Colors color;
    Score score;

    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("triggered");
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Blob") //eww
        {
            Debug.Log(other.gameObject.GetComponent<Steering>().color.ToString());
            if (other.gameObject.GetComponent<Steering>().color == color)
            {
                score.ChangeScore(10);
                other.GetComponent<Steering>().die();
               
            }

        }
    }
}
