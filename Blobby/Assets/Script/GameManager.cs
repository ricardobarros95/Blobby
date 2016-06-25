using UnityEngine;
using System.Collections;


public enum Colors { GREEN, RED, BLUE, ORANGE, PURPLE, YELLOW };

public class GameManager : MonoBehaviour {

    public GameObject[] goalObjects = new GameObject[2];


    public static Color orange = new Color(1, 0.647059f, 0, 1);
    public static Color purple = new Color(0.627451f, 0.12549f, 0.941176f, 1);  

    void Start()
    {
        InvokeRepeating("PickGoals", 0, 3);
    }
    
    void PickGoals()
    {
        int goals = Random.Range(0, 3);
        if (goals == 0)
        {
            goals = Random.Range(0, 2);
            if(goals == 0)
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.red;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.green;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.RED;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.GREEN;
            }
            else
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.green;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.red;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.GREEN;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.RED;
            }
        }
        else if( goals == 1)
        {
            goals = Random.Range(0, 1);
            if(goals == 0)
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.blue;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = orange;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.BLUE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.ORANGE;
            }
            else
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = orange;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.blue;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.ORANGE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.BLUE;
            }
        }
        else
        {
            goals = Random.Range(0, 1);
            if(goals == 0)
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = purple;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.yellow;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.PURPLE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.YELLOW;
            }
            else
            {
                goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
                goalObjects[1].GetComponent<MeshRenderer>().material.color = purple;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.YELLOW;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.PURPLE;
            }
        }
    }



}
