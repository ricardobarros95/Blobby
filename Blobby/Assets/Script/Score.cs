using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score;
    public GameObject textObject;
    Text scoreText;
    Data data;

    void Start()
    {
        scoreText = textObject.GetComponent<Text>();
        var dObj = GameObject.Find("Data"); 
        if( dObj!= null )
            data = dObj.GetComponent<Data>();
    }

    public void ChangeScore(int quantity)
    {
        score += quantity;
        ChangeScoreUI();
        Handheld.Vibrate();
        if(data != null)
            data.score = score;
    }

    void ChangeScoreUI()
    {
        scoreText.text = score.ToString();
    }

}
