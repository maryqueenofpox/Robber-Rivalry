using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapParts : MonoBehaviour
{
    public bool iAmTiredOfWaitingOnGem = true;
    public GameObject gemPlatform;

    //[SerializeField]
    public List<GameObject> startingPieces = new List<GameObject>(); // holds a list of the ground cubes
    [SerializeField]
    List<GameObject> swappablePieces = new List<GameObject>(); // holds a list of the cubes used for the swap

    // for holding the position value
    Transform groundPiece;
    Transform swapPiece;

    // for holding a constant value for the position to swap to
    Vector3 groundPieceVector;
    Vector3 swapPieceVector;

    // bools to make sure certain functions don't get called
    public bool isSwapping = false;
    public bool finishedSwapping;

    // holds the value of the index value for the list to use later
    int randomIndex;
    int randomSwapIndex;

    [SerializeField] float swapTimer;
    float originalTimer;

    [SerializeField] float swapSpeed = 50f;

    float swap = 0.2f; // for updating the scan only once
    public GameObject index { get; private set; }

    bool warning;
    public float warningTimer = 5f;
    float originalWarningTimer;

    //public Color originalColour { get; private set; }
    //public Color originalColour2 { get; private set; }

    //bool pickedColour;
    bool changedColour;


    [SerializeField] float flashTimerSpeed;
    [SerializeField] float lowTimeFlashTimerSpeed;
    [SerializeField] float whenToMakeFastFlash;

    public float lowTimeFlashForEndFall { get; private set; }

    bool toDropAllPlatforms;

    float[] rotationDegrees = { 0.0f, 90.0f, 180.0f, 270.0f };
    int rotationArrayLength;

    public bool endGame { get; private set; }
    [SerializeField] Timer timer;
    [SerializeField] EndGame endGameScript;

    [SerializeField] Material platformWarningMaterial;
    public Material warningForEndGamePlatformFall { get; private set; }
    public Material originalMaterialOnPlatform1st { get; private set; }
    public Material originalMaterialOnPlatform2nd { get; private set; }

    private void Start()
    {
        originalTimer = swapTimer;
        warning = false;
        originalWarningTimer = warningTimer;
        //pickedColour = false;
        changedColour = false;

        toDropAllPlatforms = false;
        rotationArrayLength = rotationDegrees.Length;

        endGame = false;

        warningForEndGamePlatformFall = platformWarningMaterial;
        lowTimeFlashForEndFall = lowTimeFlashTimerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timer <= endGameScript.fuseTimer && !isSwapping)
        {
            endGame = true;
        }

        if (!endGame)
        {
            GetInput(); // keeps checking for any sort of input
            SwapGroundPieces(); // keeps updating the position of the ground piece
            SwapSkyPieces(); // keeps updating the position of the swap piece

            if (warning)
            {
                warningTimer -= Time.deltaTime;

                FlashTheWarning();
            }

            if (warningTimer <= 0)
            {
                isSwapping = true; // sets the bool to be true which will stop further swapping until set to false
                warningTimer = originalWarningTimer;
                warning = false;
                changedColour = false;
                StopAllCoroutines();
            }
        }
        else
            return;
    }

    void GetInput()
    {
        if (!warning && !isSwapping)
            swapTimer -= Time.deltaTime;

        if (swapTimer <= 0.0f)
        {
            BeginSwap();
            swapTimer = originalTimer;
        }
    }

    void BeginSwap()
    {
        PickRandomGround(); // picks a random cube to swap
        PickRandomSwap(); // picks a random swap cube to swap with

        /*
        if (!pickedColour)
        {
            Material[] materials = index.GetComponent<Renderer>().materials;
            originalColour = materials[1].color;
            originalColour2 = materials[2].color;
            pickedColour = true;
        }
        */

        warning = true;
    }

    void PickRandomGround()
    {
        randomIndex = Random.Range(0, startingPieces.Count); // picks a random index number from the list
        index = startingPieces[randomIndex]; // sets the index to be the random picked object from the list
        groundPiece = index.transform; // makes the groundPiece hold the value of the transform
        groundPieceVector = groundPiece.position; // the vector will hold a constant value of the current position

        Material[] materials = index.GetComponent<Renderer>().materials;

        originalMaterialOnPlatform1st = materials[1];
        originalMaterialOnPlatform2nd = materials[2];

        /*if (groundPiece.gameObject.GetComponent<CheckCollision>().isColliding) // if the player is on the box that got picked
            PickRandomGround(); // redo the random generation*/
    }

    void PickRandomSwap()
    {
        if (iAmTiredOfWaitingOnGem)
        {
            swapPiece = gemPlatform.transform;
            iAmTiredOfWaitingOnGem = false;
            swapPieceVector = swapPiece.position;
            randomSwapIndex = 7;
        }
        else
        {
            // Same description as the one above but instead it's for the pieces to swap with
            randomSwapIndex = Random.Range(0, swappablePieces.Count);
            GameObject swapIndex = swappablePieces[randomSwapIndex];
            swapPiece = swapIndex.transform;
            swapPieceVector = swapPiece.position;
        }
    }

    void SwapGroundPieces()
    {
        if (isSwapping) // if it's true
        {
            if (finishedSwapping == true) // if this piece finished swapping
            {
                return; // don't go further
            }

            // Changes the position of the piece to smoothly move to the position of the swap piece
            groundPiece.position = Vector3.MoveTowards(groundPiece.position, swapPieceVector, Time.deltaTime * swapSpeed);

            Material[] materials = index.GetComponent<Renderer>().materials;

            materials[1] = originalMaterialOnPlatform1st;
            materials[2] = originalMaterialOnPlatform2nd;

            index.GetComponent<Renderer>().materials = materials;

            swap -= Time.deltaTime;
            if (swap <= 0f)
            {
                AstarPath.active.Scan();
                swap = 10000f;
            }

            if (groundPiece.position == swapPieceVector) // if the position of the ground and swap piece is the same
            {
                swappablePieces.Add(startingPieces[randomIndex]); // adds the ground piece to the swap piece list
                startingPieces.RemoveAt(randomIndex); // removes the ground piece from the ground piece list
                finishedSwapping = true; // sets it to true

                int rotationIndex = Random.Range(0, rotationArrayLength);
                swapPiece.Rotate(0.0f, rotationDegrees[rotationIndex], 0.0f);

                swap = 0.2f;
            }
        }
    }

    void SwapSkyPieces()
    {
        if (isSwapping && finishedSwapping) // if the swap is still happening and the other piece finished swapping
        {
            // move the swap piece to the position where the ground piece was
            swapPiece.position = Vector3.MoveTowards(swapPiece.position, groundPieceVector, Time.deltaTime * swapSpeed);

            if (swapPiece.position == groundPieceVector) // if the position matches where the ground piece was
            {
                AstarPath.active.Scan();
                // set the bools to false to allow for further swaps in the future
                isSwapping = false;
                // swap the location of the swap piece between the lists
                startingPieces.Add(swappablePieces[randomSwapIndex]);
                swappablePieces.RemoveAt(randomSwapIndex);
                finishedSwapping = false;
            }
        }
    }

    IEnumerator FlashingColours()
    {
        if (warningTimer > whenToMakeFastFlash)
        {
            changedColour = true;
            yield return new WaitForSeconds(flashTimerSpeed);
            changedColour = false;
        }
        else
        {
            changedColour = true;
            yield return new WaitForSeconds(lowTimeFlashTimerSpeed);
            changedColour = false;
        }
    }

    void FlashTheWarning()
    {
        Material[] materials = index.GetComponent<Renderer>().materials;

        if (!toDropAllPlatforms)
        {
            if (!changedColour)
            {
                if (materials[1] == originalMaterialOnPlatform1st)
                {
                    materials[1] = platformWarningMaterial;
                    materials[2] = platformWarningMaterial;
                    index.GetComponent<Renderer>().materials = materials;
                }
                else
                {
                    materials[1] = originalMaterialOnPlatform1st;
                    materials[2] = originalMaterialOnPlatform2nd;
                    index.GetComponent<Renderer>().materials = materials;
                }
                StartCoroutine(FlashingColours());
            }
        }
    }
}