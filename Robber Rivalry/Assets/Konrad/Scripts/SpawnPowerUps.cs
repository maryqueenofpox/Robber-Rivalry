using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] GameObject[] powerUps;
    public List<GameObject> testList = new List<GameObject>();
    GameObject platformToSpawnOn;
    Transform randomChild;

    float powerUpSpawnTimer = 10f;
    float originalSpawnTimer;

    int powerUpIndex;
    int platformIndex;
    int randomChildIndex;

    [SerializeField] SwapParts swapPartsScript;

    private void Start()
    {
        originalSpawnTimer = powerUpSpawnTimer;
        testList.AddRange(swapPartsScript.startingPieces);
    }

    private void Update()
    {
        if (swapPartsScript.isSwapping)
        {
            testList.Clear();
            testList.AddRange(swapPartsScript.startingPieces);
        }
        

        powerUpSpawnTimer -= Time.deltaTime;
        if (powerUpSpawnTimer <= 0f)
        {
            powerUpIndex = Random.Range(0, powerUps.Length);
            platformIndex = Random.Range(0, swapPartsScript.startingPieces.Count);
            platformToSpawnOn = swapPartsScript.startingPieces[platformIndex];
            randomChildIndex = Random.Range(0, platformToSpawnOn.transform.childCount);
            randomChild = platformToSpawnOn.transform.GetChild(randomChildIndex);

            Instantiate(powerUps[powerUpIndex], randomChild.position, Quaternion.identity);
            powerUpSpawnTimer = originalSpawnTimer;
        }
    }
}