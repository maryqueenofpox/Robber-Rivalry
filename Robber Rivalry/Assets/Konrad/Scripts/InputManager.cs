using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputManager manager;
    [SerializeField] List<GameObject> players = new List<GameObject>();

    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        manager = GetComponent<PlayerInputManager>();
        manager.playerPrefab = players[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        player.transform.position = transform.position;
    }

    public void SwitchCharacterPrefab(PlayerInput player)
    {
        ++index;

        if (index > players.Count - 1)
            index = 0;

        manager.playerPrefab = players[index];
    }
}
