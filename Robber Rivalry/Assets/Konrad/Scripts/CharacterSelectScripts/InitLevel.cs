using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevel : MonoBehaviour
{
    [SerializeField] Transform[] playerSpawns;
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var PlayerConfigs = CharacterSelect.instance.GetPlayerConfigs().ToArray();

        for (int i = 0; i < PlayerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitPlayer(PlayerConfigs[i]);
        }
    }
}
