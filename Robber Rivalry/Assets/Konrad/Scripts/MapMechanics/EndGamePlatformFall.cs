using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public GameObject winningPlatform { get; private set; }
    [SerializeField] Material winningPlatformMaterial;

    [SerializeField] GameObject killzone;

    bool doFall;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        getPlatforms = true;
        pickedRandom = false;
        doFall = false;
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
            killzone.SetActive(false);
        }

        if (!pickedRandom && !getPlatforms)
        {
            int random = Random.Range(0, platforms.Count);

            winningPlatform = platforms[random];

            Material[] platformThatBreaksGreen = swapPartsScript.index.GetComponent<Renderer>().materials;
            platformThatBreaksGreen[1] = swapPartsScript.originalMaterialOnPlatform1st;
            platformThatBreaksGreen[2] = swapPartsScript.originalMaterialOnPlatform2nd;
            swapPartsScript.index.GetComponent<Renderer>().materials = platformThatBreaksGreen;

            Material[] materials = winningPlatform.GetComponent<Renderer>().materials;

            materials[1] = winningPlatformMaterial;
            materials[2] = winningPlatformMaterial;

            winningPlatform.GetComponent<Renderer>().materials = materials;

            platforms.RemoveAt(random);

            if (random < 6)
            {
                platforms.Reverse();
            }

            endGameObject.transform.parent = winningPlatform.transform;
            endGameObject.transform.position = winningPlatform.transform.position;

            pickedRandom = true;
            Debug.Log("Before calling thingy thingy, this should appear only ocne");
            StartCoroutine(FlashTheSequence());
        }

        if (!(platforms.Count == 0) && doFall)
        {
            if (swapPartsScript.endGame && !getPlatforms && pickedRandom && index <= platforms.Count - 1)
            {
                MakePlatformsFallUpToDown();
            }
        }

        Material[] mats = platforms[index + 1].GetComponent<Renderer>().materials;

        mats[1] = swapPartsScript.warningForEndGamePlatformFall;
        mats[2] = swapPartsScript.warningForEndGamePlatformFall;

        platforms[index + 1].GetComponent<Renderer>().materials = mats;
    }

    void MakePlatformsFallUpToDown()
    {
        Debug.Log("Index: " + index);

        if (index > 0)
            index = 0;

        if (platforms[index].transform.position == positionToFallTo.position)
        {
            platforms.RemoveAt(index);
        }

        platforms[index].transform.position = Vector3.MoveTowards(platforms[index].transform.position, positionToFallTo.position, fallSpeed * Time.deltaTime);
    }

    IEnumerator FlashTheSequence()
    {
        while(!doFall)
        {
            Debug.Log("While is calling");

            Debug.Log("FlashTheSequence Total Count");

            if (index > platforms.Count - 1)
            {
                Debug.Log("FlashTheSequence Stop Count");

                index = 0;

                Material[] mats2 = platforms[index].GetComponent<Renderer>().materials;

                mats2[1] = swapPartsScript.warningForEndGamePlatformFall;
                mats2[2] = swapPartsScript.warningForEndGamePlatformFall;

                platforms[index].GetComponent<Renderer>().materials = mats2;

                yield return new WaitForSeconds(1.0f);
                StopCoroutine(FlashTheSequence());
                doFall = true;
            }

            Material[] mats = platforms[index].GetComponent<Renderer>().materials;

            mats[1] = swapPartsScript.warningForEndGamePlatformFall;
            mats[2] = swapPartsScript.warningForEndGamePlatformFall;

            platforms[index].GetComponent<Renderer>().materials = mats;

            yield return new WaitForSeconds(0.1f);

            mats[1] = swapPartsScript.originalMaterialOnPlatform1st;
            mats[2] = swapPartsScript.originalMaterialOnPlatform2nd;

            platforms[index].GetComponent<Renderer>().materials = mats;

            index++;
        }
    }
}