using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    List<PlayerConfig> playerConfigs;

    [SerializeField] int maxPlayers = 4;

    public static CharacterSelect instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Trying to create another instance");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfig>();
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColour(int index, Material color)
    {
        playerConfigs[index].playerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;

        if (playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("CharacterTest");
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player joined " + playerInput.playerIndex);
        
        if (!playerConfigs.Any(p=> p.playerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfig(playerInput));
        }
    }
}

public class PlayerConfig
{
    public PlayerConfig(PlayerInput playerInput)
    {
        playerIndex = playerInput.playerIndex;
        input = playerInput;
    }

    public PlayerInput input;
    public int playerIndex;
    public bool isReady;
    public Material playerMaterial;
}
