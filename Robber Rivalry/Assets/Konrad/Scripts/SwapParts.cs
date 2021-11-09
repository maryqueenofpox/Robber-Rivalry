using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapParts : MonoBehaviour
{
    [SerializeField]
    List<GameObject> startingPieces = new List<GameObject>(); // holds a list of the ground cubes
    [SerializeField]
    List<GameObject> swappablePieces = new List<GameObject>(); // holds a list of the cubes used for the swap

    // for holding the position value
    Transform groundPiece;
    Transform swapPiece; 

    // for holding a constant value for the position to swap to
    Vector3 groundPieceVector; 
    Vector3 swapPieceVector;

    // bools to make sure certain functions don't get called
    bool isSwapping = false;
    bool finishedSwapping;

    // holds the value of the index value for the list to use later
    int randomIndex;
    int randomSwapIndex;

    // Update is called once per frame
    void Update()
    {
        GetInput(); // keeps checking for any sort of input
        SwapGroundPieces(); // keeps updating the position of the ground piece
        SwapSkyPieces(); // keeps updating the position of the swap piece
    }

    void GetInput()
    {
        if (Keyboard.current.spaceKey.IsPressed() && isSwapping == false)
        {
            BeginSwap(); // when the key gets called it calls the BeginSwap method
        }
    }

    void BeginSwap()
    {
        PickRandomGround(); // picks a random cube to swap
        PickRandomSwap(); // picks a random swap cube to swap with
        
        isSwapping = true; // sets the bool to be true which will stop further swapping until set to false
    }

    void PickRandomGround()
    {
        randomIndex = Random.Range(0, startingPieces.Count); // picks a random index number from the list
        GameObject index = startingPieces[randomIndex]; // sets the index to be the random picked object from the list
        groundPiece = index.transform; // makes the groundPiece hold the value of the transform
        groundPieceVector = groundPiece.position; // the vector will hold a constant value of the current position

        if (groundPiece.gameObject.GetComponent<CheckCollision>().isColliding) // if the player is on the box that got picked
            PickRandomGround(); // redo the random generation
    }

    void PickRandomSwap()
    {
        // Same description as the one above but instead it's for the pieces to swap with
        randomSwapIndex = Random.Range(0, swappablePieces.Count);
        GameObject swapIndex = swappablePieces[randomSwapIndex];
        swapPiece = swapIndex.transform;
        swapPieceVector = swapPiece.position;
    }

    void SwapGroundPieces()
    {
        if (isSwapping) // if it's true
        {
            if (finishedSwapping == true) // if this piece finished swapping
                return; // don't go further

            // Changes the position of the piece to smoothly move to the position of the swap piece
            groundPiece.position = Vector3.MoveTowards(groundPiece.position, swapPieceVector, Time.deltaTime * 10f);

            if (groundPiece.position == swapPieceVector) // if the position of the ground and swap piece is the same
            {
                finishedSwapping = true; // sets it to true
                swappablePieces.Add(startingPieces[randomIndex]); // adds the ground piece to the swap piece list
                startingPieces.RemoveAt(randomIndex); // removes the ground piece from the ground piece list
            }
        }
    }

    void SwapSkyPieces()
    {
        if (isSwapping && finishedSwapping) // if the swap is still happening and the other piece finished swapping
        {
            // move the swap piece to the position where the ground piece was
            swapPiece.position = Vector3.MoveTowards(swapPiece.position, groundPieceVector, Time.deltaTime * 10f);

            if (swapPiece.position == groundPieceVector) // if the position matches where the ground piece was
            {
                // set the bools to false to allow for further swaps in the future
                finishedSwapping = false;
                isSwapping = false;
                // swap the location of the swap piece between the lists
                startingPieces.Add(swappablePieces[randomSwapIndex]);
                swappablePieces.RemoveAt(randomSwapIndex);
            }
        }
    }
}
