using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public int score;

    public void ChangeScore(int quantity)
    {
        score += quantity;
    }
}
