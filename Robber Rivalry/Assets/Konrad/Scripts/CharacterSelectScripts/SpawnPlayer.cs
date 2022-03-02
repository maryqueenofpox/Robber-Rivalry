using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerSetupMenu;
    public PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("MainCanvas");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenu, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetup>().SetPlayerIndex(input.playerIndex);
        }
    }
}
