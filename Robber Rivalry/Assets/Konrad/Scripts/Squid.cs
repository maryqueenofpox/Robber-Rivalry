using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    [SerializeField] List<GameObject> tentacles = new List<GameObject>();
    [SerializeField] List<GameObject> tentaclesWaypoints = new List<GameObject>();

    [SerializeField] float slamSpeed = 5f;
    [SerializeField] float lowSlapTime = 1f;
    [SerializeField] float highSlapTime = 5f;

    List<Vector3> originalPositions = new List<Vector3>();

    int tenticleIndex = 0;
    float timer;

    bool doingSlap;
    bool pickedRandomTentacle;
    bool moveToOrigin;
    bool pickedTime;

    // Start is called before the first frame update
    void Start()
    {
        doingSlap = false;
        pickedRandomTentacle = false;
        moveToOrigin = false;
        pickedTime = false;

        foreach (GameObject item in tentacles)
        {
            originalPositions.Add(item.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I do things");

        if (doingSlap)
        {
            if (!pickedRandomTentacle)
                PickRandomTentacle();
            else
                UseRandomTentacle();
        }
        else
        {
            if (!pickedTime)
                PickRandomTime();
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    doingSlap = true;
                    pickedRandomTentacle = false;
                    pickedTime = false;
                }
            }
        }
    }

    void PickRandomTime()
    {
        timer = Random.Range(lowSlapTime, highSlapTime);
        pickedTime = true;
    }

    void PickRandomTentacle()
    {
        Debug.Log("Picking");
        if (!pickedRandomTentacle)
        {
            tenticleIndex = Random.Range(0, tentacles.Count);
            Debug.Log("Picked");
            pickedRandomTentacle = true;
        }
    }

    void UseRandomTentacle()
    {
        Debug.Log("UseRandom");

        if (!moveToOrigin)
        {
            Debug.Log("False Origin");
            tentacles[tenticleIndex].transform.position = Vector3.MoveTowards(tentacles[tenticleIndex].transform.position, tentaclesWaypoints[tenticleIndex].transform.localPosition, slamSpeed * Time.deltaTime);

            if (tentacles[tenticleIndex].transform.position == tentaclesWaypoints[tenticleIndex].transform.localPosition)
                moveToOrigin = true;
        }

        if (moveToOrigin)
        {
            Debug.Log("True Origin");
            tentacles[tenticleIndex].transform.position = Vector3.MoveTowards(tentacles[tenticleIndex].transform.position, originalPositions[tenticleIndex], slamSpeed * Time.deltaTime);

            if (tentacles[tenticleIndex].transform.position == originalPositions[tenticleIndex])
            {
                doingSlap = false;
                moveToOrigin = false;
            }
        }
    }
}