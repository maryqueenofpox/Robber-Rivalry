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
    int indexOfWinningPlatform;
    bool rearrangedList;
    int loopTracker;
    bool doFlashingOnce;

    [Header("Platform Fall Paterns")]
    [SerializeField] List<int> ifPlatform1 = new List<int>(11);
    [SerializeField] List<int> ifPlatform2 = new List<int>(11);
    [SerializeField] List<int> ifPlatform3 = new List<int>(11);
    [SerializeField] List<int> ifPlatform4 = new List<int>(11);
    [SerializeField] List<int> ifPlatform5 = new List<int>(11);
    [SerializeField] List<int> ifPlatform6 = new List<int>(11);
    [SerializeField] List<int> ifPlatform7 = new List<int>(11);
    [SerializeField] List<int> ifPlatform8 = new List<int>(11);
    [SerializeField] List<int> ifPlatform9 = new List<int>(11);
    [SerializeField] List<int> ifPlatform10 = new List<int>(11);
    [SerializeField] List<int> ifPlatform11 = new List<int>(11);
    [SerializeField] List<int> ifPlatform12 = new List<int>(11);

    List<GameObject> temporaryPlatforms = new List<GameObject>(11);

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        getPlatforms = true;
        pickedRandom = false;
        doFall = false;
        rearrangedList = false;
        doFlashingOnce = false;
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
            indexOfWinningPlatform = Random.Range(0, platforms.Count);

            winningPlatform = platforms[indexOfWinningPlatform];

            Material[] platformThatBreaksGreen = swapPartsScript.index.GetComponent<Renderer>().materials;
            platformThatBreaksGreen[1] = swapPartsScript.originalMaterialOnPlatform1st;
            platformThatBreaksGreen[2] = swapPartsScript.originalMaterialOnPlatform2nd;
            swapPartsScript.index.GetComponent<Renderer>().materials = platformThatBreaksGreen;

            Material[] materials = winningPlatform.GetComponent<Renderer>().materials;

            materials[1] = winningPlatformMaterial;
            materials[2] = winningPlatformMaterial;

            winningPlatform.GetComponent<Renderer>().materials = materials;

            platforms.RemoveAt(indexOfWinningPlatform);

            endGameObject.transform.parent = winningPlatform.transform;
            endGameObject.transform.position = winningPlatform.transform.position;

            pickedRandom = true;
        }

        if (pickedRandom && !rearrangedList)
        {
            switch (indexOfWinningPlatform)
            {
                case 0:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform1);
                    break;
                case 1:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform2);
                    break;
                case 2:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform3);
                    break;
                case 3:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform4);
                    break;
                case 4:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform5);
                    break;
                case 5:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform6);
                    break;
                case 6:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform7);
                    break;
                case 7:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform8);
                    break;
                case 8:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform9);
                    break;
                case 9:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform10);
                    break;
                case 10:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform11);
                    break;
                case 11:
                    RearrangeTheListToFallInSpecifiedOrder(ifPlatform12);
                    break;
                default:
                    Debug.Log("ERROR: Switch case statement for EndGamePlatformFall has inccured an issue with value being out of bounds.");
                    break;
            }
        }

        if (rearrangedList && !doFlashingOnce)
        {
            StartCoroutine(FlashTheSequence());
            doFlashingOnce = true;
        }

        if (!(platforms.Count == 0) && doFall)
        {
            if (swapPartsScript.endGame && !getPlatforms && pickedRandom && index <= platforms.Count - 1)
            {
                MakePlatformsFall();
            }

            Material[] mats = platforms[index + 1].GetComponent<Renderer>().materials;

            mats[1] = swapPartsScript.warningForEndGamePlatformFall;
            mats[2] = swapPartsScript.warningForEndGamePlatformFall;

            platforms[index + 1].GetComponent<Renderer>().materials = mats;
        }
    }

    void RearrangeTheListToFallInSpecifiedOrder(List<int> listToOrderBy)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            temporaryPlatforms.Add(platforms[listToOrderBy[i]]);
            loopTracker++;
        }

        if (loopTracker > platforms.Count - 1)
        {
            platforms.Clear();
            platforms.AddRange(temporaryPlatforms);
            rearrangedList = true;
        }
    }

    void MakePlatformsFall()
    {
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
            if (index > platforms.Count - 1)
            {
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