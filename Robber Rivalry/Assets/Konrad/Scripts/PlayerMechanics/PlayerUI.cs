using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject powerUpUI;
    [SerializeField] Image sprintBar;

    PlayerAbilities playerAbilities;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
        powerUpUI.SetActive(false);

        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAbilities.canUseAbility)
            powerUpUI.SetActive(true);
        else
            powerUpUI.SetActive(false);

        sprintBar.fillAmount = playerMovement.dashDuration / playerMovement.maxDashTime;
        if ((playerMovement.dashDuration / playerMovement.maxDashTime) < 0.5f)
            sprintBar.color = Color.red;
        else
            sprintBar.color = Color.green;
    }

    public void DoShitToMenu()
    {
        if (menuPanel.activeInHierarchy == true)
        {
            menuPanel.SetActive(false);
            Debug.Log("Off");
        }
        else if (menuPanel.activeInHierarchy == false)
        {
            menuPanel.SetActive(true);
            Debug.Log("On");
        }
    }
}
