using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePlatformFall : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public List<GameObject> cubes = new List<GameObject>();

    [SerializeField] SwapParts swapPartsScript;
    [SerializeField] Transform positionToFallTo;
    [SerializeField] float fallSpeed;

    bool getPlatforms;
    bool pickedRandom;

    int index;

    bool nextPlatform;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        nextPlatform = false;
        getPlatforms = true;
        pickedRandom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (swapPartsScript.endGame && getPlatforms)
        {
            foreach (GameObject item in cubes)
            {
                platforms.Add(item.GetComponent<CubeChecker>().nameOfObject);
            }

            getPlatforms = false;
        }

        if (!pickedRandom && !getPlatforms)
        {
            int random = Random.Range(0, platforms.Count);
            platforms.RemoveAt(random);
            pickedRandom = true;
        }

        if (swapPartsScript.endGame && !getPlatforms && pickedRandom)
            MakePlatformsFall();
    }

    void MakePlatformsFall()
    {
        platforms[index].transform.position = Vector3.MoveTowards(platforms[index].transform.position, positionToFallTo.position, fallSpeed * Time.deltaTime);

        if (platforms[index].transform.position == positionToFallTo.position)
            index++;
    }
}
