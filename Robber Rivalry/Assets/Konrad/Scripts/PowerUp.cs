using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Code that I decided to scrap as apparently there was a simpler solution
    // Decided to keep it as maybe some parts can come useful
    /*[SerializeField]
    GameObject[] players;
    PlayerControls[] playerControls;

    bool playerJoined = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerControls = new PlayerControls[players.Length];
        for (int index = 0; index < players.Length; index++)
        {
            playerControls[index] = players[index].GetComponent<PlayerControls>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayerObject();

        Debug.Log("Objects: " + players.Length);
    }

    public void FindPlayerObject()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerControls = new PlayerControls[players.Length];
        for (int index = 0; index < players.Length; index++)
        {
            playerControls[index] = players[index].GetComponent<PlayerControls>();
        }
        playerJoined = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject whichObjectCollided = players[playerControls];

        if (!playerControls[players.Length - 1].canUseAbility)
        {
            playerControls[0].canUseAbility = true;
            gameObject.SetActive(false);
        }

        playerControls[GetInstanceID()].canUseAbility = true;
        gameObject.SetActive(false);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}