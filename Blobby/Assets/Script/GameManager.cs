using UnityEngine;
using System.Collections;


public enum Colors { GREEN, RED, BLUE, ORANGE, PURPLE, YELLOW, BLACK, UNKN };

public class GameManager : MonoBehaviour {

    public GameObject[] goalObjects = new GameObject[2];


    public static Color orange = new Color32(0xf0,0x72,0x00,0xff);
    public static Color purple = new Color32(0xd2, 0x10, 0xbc, 0xff);
    public static Color red = new Color32(0xce, 0x1e, 0x0a, 0xff);
    public static Color yellow= new Color32(0xf0, 0xe0, 0x0a, 0xff);
    public static Color blue = new Color32(0x34, 0x2d, 0xee, 0xff);
    public static Color green = new Color32(0x19, 0xaa, 0x32, 0xff);

    public Sprite[] colorSprites;
    public GameObject goalSprite1;
    public GameObject goalSprite2;

    void Start()
    {
        InvokeRepeating("PickGoals", 0, 3);
        
    }
    void GoalsOn()
    {
        goalObjects[0].SetActive(true);
        goalObjects[1].SetActive(true);
    }

    void PickGoals()
    {
        int goals = Random.Range(0, 3);
        if (goals == 0)
        {
            goals = Random.Range(0, 2);
            if(goals == 0)
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.red;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.green;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.RED;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.GREEN;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[1];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[0];


            }
            else
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.green;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.red;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.GREEN;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.RED;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[0];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[1];
            }
        }
        else if( goals == 1)
        {
            goals = Random.Range(0, 2);
            if(goals == 0)
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.blue;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = orange;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.BLUE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.ORANGE;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[2];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[3];
            }
            else
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = orange;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.blue;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.ORANGE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.BLUE;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[3];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[2];
            }
        }
        else
        {
            goals = Random.Range(0, 2);
            if(goals == 0)
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = purple;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = Color.yellow;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.PURPLE;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.YELLOW;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[4];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[5];
            }
            else
            {
                //goalObjects[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
                //goalObjects[1].GetComponent<MeshRenderer>().material.color = purple;
                goalObjects[0].GetComponent<GoalColor>().color = Colors.YELLOW;
                goalObjects[1].GetComponent<GoalColor>().color = Colors.PURPLE;
                goalSprite1.GetComponent<SpriteRenderer>().sprite = colorSprites[5];
                goalSprite2.GetComponent<SpriteRenderer>().sprite = colorSprites[4];
            }
        }
        goalObjects[0].SetActive(false);
        goalObjects[1].SetActive(false);
        Invoke("GoalsOn", 0.3f);

    }



}
