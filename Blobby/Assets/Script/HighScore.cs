using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    Text highScore;
    Data data;

    void Start()
    {
        data = GameObject.Find("Data").GetComponent<Data>();
        highScore = GetComponent<Text>();
        highScore.text = data.score.ToString();
    }

}
