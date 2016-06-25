using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score;
    public GameObject textObject;
    Text scoreText;

    void Start()
    {
        scoreText = textObject.GetComponent<Text>();
    }

    public void ChangeScore(int quantity)
    {
        score += quantity;
        ChangeScoreUI();
    }

    void ChangeScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
