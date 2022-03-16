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

    bool createOneCollider;

    public GameObject winningPlatform { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        getPlatforms = true;
        pickedRandom = false;
        createOneCollider = false;
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
            MakePlatformsFall();
    }

    void MakePlatformsFall()
    {
        platforms[index].transform.position = Vector3.MoveTowards(platforms[index].transform.position, positionToFallTo.position, fallSpeed * Time.deltaTime);

        if (platforms[index].transform.position == positionToFallTo.position)
            index++;
    }
}
