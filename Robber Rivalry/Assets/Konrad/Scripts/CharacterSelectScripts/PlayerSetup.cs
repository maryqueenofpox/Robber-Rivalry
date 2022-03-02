using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour
{
    int playerIndex;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject readyPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] Button readyButton;

    float ignoreInputTime = 1.5f;
    bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1.ToString()));
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetColour(Material colour)
    {
        if (!inputEnabled) { return; }

        CharacterSelect.instance.SetPlayerColour(playerIndex, colour);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        CharacterSelect.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
