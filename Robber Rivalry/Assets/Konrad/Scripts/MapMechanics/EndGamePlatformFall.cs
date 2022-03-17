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
    [SerializeField] GameObject endGameObject;

    bool getPlatforms;
    bool pickedRandom;

    int index;
    int colourIndex;

    bool nextPlatform;

    public GameObject winningPlatform { get; private set; }

    EndGame endGameScript;
    bool once;

    // Start is called before the first frame update
    void Start()
    {
        endGameScript = GetComponent<EndGame>();
        index = 0;
        colourIndex = 0;
        getPlatforms = true;
        pickedRandom = false;
        nextPlatform = false;
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (endGameScript.fuse.enabled && !once)
        {
            Material[] materialsRed = platforms[++colourIndex].GetComponent<Renderer>().materials;
            materialsRed[1].color = Color.red;
            materialsRed[2].color = Color.red;

            once = true;
        }

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
            winningPlatform = platforms[random];

            Material[] materials = winningPlatform.GetComponent<Renderer>().materials;
            materials[1].color = Color.green;
            materials[2].color = Color.green;

            platforms.RemoveAt(random);
            pickedRandom = true;

            endGameObject.transform.parent = winningPlatform.transform;
            endGameObject.transform.position = winningPlatform.transform.position;
        }

        if (swapPartsScript.endGame && !getPlatforms && pickedRandom && index < platforms.Count)
        {
            if (nextPlatform)
            {
                Material[] materialsRed = platforms[++colourIndex].GetComponent<Renderer>().materials;
                materialsRed[1].color = Color.red;
                materialsRed[2].color = Color.red;
            }
            

            
            MakePlatformsFall();
        }
    }

    void MakePlatformsFall()
    {
        platforms[index].transform.position = Vector3.MoveTowards(platforms[index].transform.position, positionToFallTo.position, fallSpeed * Time.deltaTime);
        nextPlatform = false;

        if (platforms[index].transform.position == positionToFallTo.position)
        {
            nextPlatform = true;
            index++;
        }
    }
}
