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
                    PewGun(true);
                    break;
                case 1:
                    Sign(true);
                    break;
                case 2:
                    Honey(true);
                    break;
                case 3:
                    Shield(true);
                    break;
                case 4:
                    Magnet(true);
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

    public void PewGun(bool enabled)
    {
        pewImage.enabled = enabled;

        signImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Sign(bool enabled)
    {
        signImage.enabled = enabled;

        pewImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Honey(bool enabled)
    {
        honeyImage.enabled = enabled;

        signImage.enabled = false;
        pewImage.enabled = false;
        shieldImage.enabled = false;
        magnetImage.enabled = false;
    }

    public void Shield(bool enabled)
    {
        shieldImage.enabled = enabled;

        signImage.enabled = false;
        pewImage.enabled = false;
        magnetImage.enabled = false;
        honeyImage.enabled = false;
    }

    public void Magnet(bool enabled)
    {
        magnetImage.enabled = enabled;

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
