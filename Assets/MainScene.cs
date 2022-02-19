using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScene : MonoBehaviour
{
    public GameObject spawnObject;


    public float maxTime = 5;
    public float minTime = 2;

    public float minX=-6;
    public float maxX=-6;

    public float minY = 1;
    public float maxY = 3;

    public float minZ = 6;
    public float maxZ = 7;

    public float minSpeed = 4.0f, maxSpeed = 6.0f;

    //current time
    private float time;

    //The time to spawn the object
    private float spawnTime;

    void Start()
    {
        SetRandomTime();
        time = minTime;
    }

    void FixedUpdate()
    {

        //Counts up
        time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (time >= spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        }

    }


    //Spawns the object and resets the time
    void SpawnObject()
    {
        float xPosition = Random.Range(minX, maxX);
        float yPosition = Random.Range(minY, maxY);
        float zPosition = Random.Range(minZ, maxZ);

        time = minTime;

        Vector3 newPos = new Vector3(xPosition, yPosition, zPosition);
        

        GameObject cloned = Instantiate(spawnObject, newPos, Quaternion.Euler(90, 0, 0));
        cloned.GetComponent<animation_test>().SetSpeed(Random.Range(minSpeed, maxSpeed));
        cloned.GetComponent<animation_test>().PlaySoundThrow();
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}
