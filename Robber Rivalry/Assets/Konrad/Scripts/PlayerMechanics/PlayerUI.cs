using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    //[SerializeField] GameObject powerUpUI;
    [SerializeField] Image sprintBar;

    PlayerAbilities playerAbilities;
    PlayerMovement playerMovement;

    [SerializeField] Image signImage;
    [SerializeField] Image pewImage;
    [SerializeField] Image shieldImage;
    [SerializeField] Image magnetImage;
    [SerializeField] Image honeyImage;

    public int number { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);

        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GetComponent<PlayerMovement>();

        signImage.enabled = false;
        pewImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        sprintBar.fillAmount = playerMovement.dashDuration / playerMovement.maxDashTime;
        if ((playerMovement.dashDuration / playerMovement.maxDashTime) < 0.5f)
            sprintBar.color = Color.red;
        else
            sprintBar.color = Color.green;

        if (playerMovement.dashDuration < playerMovement.maxDashTime)
            sprintBar.enabled = true;
        else
            sprintBar.enabled = false;

        if (playerAbilities.canUseAbility)
        {
            switch (number)
            {
                case 0:
                    PewGun();
                    break;
                case 1:
                    Sign();
                    break;
                case 2:
                    Honey();
                    break;
                case 3:
                    Shield();
                    break;
                case 4:
                    Magnet();
                    break;
                default:
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        sprintBar.transform.rotation = new Quaternion(0, 0, 0, 1);
    }

    public void PewGun()
    {
        pewImage.enabled = true;

        signImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Sign()
    {
        signImage.enabled = true;

        pewImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Honey()
    {
        honeyImage.enabled = true;

        signImage.enabled = false;
        pewImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
    }

    public void Shield()
    {
        shieldImage.enabled = true;

        signImage.enabled = false;
        pewImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Magnet()
    {
        magnetImage.enabled = true;

        signImage.enabled = false;
        pewImage.enabled = false;
        shieldImage.enabled = false;
        honeyImage.enabled = false;
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
