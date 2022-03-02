using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerConfig playerConfig;

    [SerializeField] MeshRenderer meshRender;
    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    public void InitPlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        meshRender.material = pc.playerMaterial;
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        
    }
}
