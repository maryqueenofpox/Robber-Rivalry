using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    // bool that can be used in another script, but can't be set to something else
    [HideInInspector]
    public bool isColliding { get; private set; }

    // constant collision detection as long as something is on it
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // if the player is colliding
            isColliding = true; // this is true
        else
            isColliding = false; // other wise if player is not colliding, then it's false
    }
}
