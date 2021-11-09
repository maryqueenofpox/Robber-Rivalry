using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    // Might come back to this later, this random spawning is not working out too well
    /*
    float timeUntilNextObjectSpawn = 2f;
    float originalTime;
    [SerializeField] GameObject abilityToSpawn;
    [SerializeField] GameObject platformfToSpawnOn;
    float platformSizeX;
    float platformSizeZ;
    Vector3 platformCentre;
    Vector3 platformLocation;

    // Start is called before the first frame update
    void Start()
    {
        originalTime = timeUntilNextObjectSpawn;
        platformSizeX = platformfToSpawnOn.GetComponent<Collider>().bounds.size.x;
        platformSizeZ = platformfToSpawnOn.GetComponent<Collider>().bounds.size.z;
        platformLocation = platformfToSpawnOn.transform.position;

        //Debug.Log("Platform Size: " + platformSize);
        platformCentre = new Vector3(platformLocation.x, 1, platformLocation.z);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextObjectSpawn -= Time.deltaTime;
        if (timeUntilNextObjectSpawn <= 0.0f)
        {
            float platformSize = platformfToSpawnOn.GetComponent<Renderer>().bounds.size.y;
            float platformTop = platformfToSpawnOn.transform.position.y + platformSize / 2;
            Vector3 blockCentre = new Vector3(platformfToSpawnOn.transform.position.x, platformTop, platformfToSpawnOn.transform.position.z);

            //Instantiate(abilityToSpawn, new Vector3(Random.Range(0, platformSize.x), 1, Random.Range(0, platformSize.z)), Quaternion.identity);
            Instantiate(abilityToSpawn, blockCentre, Quaternion.identity);
            timeUntilNextObjectSpawn = originalTime;
        }
    }
    */
}