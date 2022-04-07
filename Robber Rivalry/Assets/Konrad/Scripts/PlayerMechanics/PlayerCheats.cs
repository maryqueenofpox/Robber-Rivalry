using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerCheats : MonoBehaviour
{
    ButtonControl[] scoreCheat = new ButtonControl[]
    {
        Gamepad.current.buttonSouth,
        Gamepad.current.buttonNorth,
        Gamepad.current.buttonEast,
        Gamepad.current.buttonWest,
        Gamepad.current.dpad.up
    };

    int buttonIndex;
    float timeForNextButton = 2f;
    [SerializeField] GameObject Player;
    float originalTimeForNextButton;

    // Start is called before the first frame update
    void Start()
    {
        
        buttonIndex = 0;
        originalTimeForNextButton = timeForNextButton;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCheat();
    }

    void ScoreCheat()
    {
        

        if (scoreCheat[buttonIndex].isPressed)
        {
            timeForNextButton = originalTimeForNextButton;
            Player.SetActive(true);
            buttonIndex++;

            if (buttonIndex > scoreCheat.Length - 1)
            {
                LootGrabber lg = GetComponent<LootGrabber>();
                lg.loot -= 100;
                lg.score.text = lg.loot.ToString();
                buttonIndex = 0;
            
            }
        }
        else
        {
            timeForNextButton -= Time.deltaTime;
            if (timeForNextButton <= 0)
                buttonIndex = 0;
        }
    }
}