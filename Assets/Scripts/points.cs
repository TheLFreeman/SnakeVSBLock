using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class points : MonoBehaviour
{
    int maxPoints = 10;
    int maxPoints2 = 25;
    int maxPoints3 = 50;
    int maxPointsMax = 100;
    public int recPoints;
    public TextMeshProUGUI display;
    public GameObject PointsPrefab;

    void Start()
    {
        GameObject snake = GameObject.Find("Head");
        snakeTail snakeTail = snake.GetComponent<snakeTail>();
        if(snakeTail.score < 10)
        {
            recPoints = Random.Range(1, maxPoints);
        }
        else if(snakeTail.score < 30)
        {
            recPoints = Random.Range(maxPoints, maxPoints2);
        }
        else if(snakeTail.score < 50)
        {
            recPoints = Random.Range(maxPoints2, maxPoints3);
        }
        else
        {
            recPoints = Random.Range(maxPoints2, maxPointsMax + snakeTail.score / 2);
        }
        // recPoints = Random.Range(snakeTail.score, snakeTail.score * 1/2);
        display.text = recPoints.ToString();
    }
}
