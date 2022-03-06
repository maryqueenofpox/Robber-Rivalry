using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashMechanic : MonoBehaviour
{
    [SerializeField] float bashForce = 2000000f;
    PlayerMovement playerMovementScript;
    PlayerMovement thisPlayer;

    bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        isPlayer = false;
        thisPlayer = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisPlayer.isDashing && isPlayer)
        {
            int randomDirection = Random.Range(0, 2);
            switch (randomDirection)
            {
                case 0:
                    playerMovementScript.rb.AddForce(-transform.right * bashForce);
                    break;
                case 1:
                    playerMovementScript.rb.AddForce(transform.right * bashForce);
                    break;
                default:
                    Debug.Log("Error, random direction error in trigger collider.");
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovementScript = other.GetComponent<PlayerMovement>();
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
