using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public List<GameObject> spawnObject;
    private int option;
    public Vector3 offset;
    private Quaternion currentAngle;
    private Vector3 currentWallPosition;
    public float delayTime;
    private float delayTimeReserve;
    public bool cubeMode;
    int count = 0; 


    void Start()
    {
        StartCoroutine(spawndelay(delayTime));
        delayTimeReserve = delayTime;
    }

    IEnumerator spawndelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(cubeMode == false)
        {
            spawnElse();
        }
        else
        spawnCube();
        StartCoroutine(spawndelay(delay)); 
    }

    private void spawnCube()
    {
        currentAngle.eulerAngles = new Vector3 (0, 90, 0);
        Instantiate(spawnObject[0], transform.position + offset, currentAngle);
    }

    // private void spawnElse()
    // {
    //     option = Random.Range(0, 2);
    //     if(option == 0)
    //     {
    //         delayTime = delayTimeReserve;
    //         currentAngle.eulerAngles = new Vector3 (0, 90, 0);
    //         Instantiate(spawnObject[option], transform.position, currentAngle);
    //     }
    //     else
    //     {
    //         delayTime = 3f;
    //         currentWallPosition = transform.position + offset;
    //         currentAngle.eulerAngles = new Vector3 (0, 0, 0);
    //         Instantiate(spawnObject[option], currentWallPosition, currentAngle);
    //     } 
    // }
    private void spawnElse()
    {
        if(count < 3)
        {
            currentAngle.eulerAngles = new Vector3 (0, 90, 0);
            Instantiate(spawnObject[0], transform.position, currentAngle);
            count++;
        }
        else
        {
            currentWallPosition = transform.position + offset;
            currentAngle.eulerAngles = new Vector3 (0, 0, 0);
            Instantiate(spawnObject[1], currentWallPosition, currentAngle);
            count = 0;
            count++; 
        }
    }
}
