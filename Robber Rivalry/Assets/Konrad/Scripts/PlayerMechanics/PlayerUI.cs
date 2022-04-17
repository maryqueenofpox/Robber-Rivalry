using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] Canvas canvas;
    //[SerializeField] GameObject powerUpUI;
    [SerializeField] Image sprintBar;

    [HideInInspector] public bool pleaseDoOnce = true;

    [SerializeField] GameObject Shield_Effect;
    [SerializeField] GameObject Fence_Effect;
    [SerializeField] GameObject Raygun_Effect;
    [SerializeField] GameObject Honey_Effect;
    [SerializeField] GameObject Magnet_Effect;

    PlayerAbilities playerAbilities;
    PlayerMovement playerMovement;

    [SerializeField] Image signImage;
    [SerializeField] Image pewImage;
    [SerializeField] Image shieldImage;
    [SerializeField] Image magnetImage;
    [SerializeField] Image honeyImage;
    public int number { get; set; }
    float ShieldETimer = 2;
    float RaygunTimer = 2;
    float FenceTimer = 2;
    float HoneyTimer = 2;
    float MagnetTimer = 2;

    //[SerializeField] GameObject pauseButtonFirst;

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
        if ((!playerMovement.canDash))
            sprintBar.color = Color.red;
        else
            sprintBar.color = Color.green;

        if (playerMovement.dashDuration < playerMovement.maxDashTime)
            sprintBar.enabled = true;
        else
            sprintBar.enabled = false;

        if (playerAbilities.canUseAbility)
        {
            if (pleaseDoOnce)
            {
                switch (number)
                {
                    case 0:
                        PewGun(true);
                        Raygun_Effect.SetActive(true);
                        pleaseDoOnce = false;
                        break;
                    case 1:
                        Sign(true);
                        Fence_Effect.SetActive(true);
                        pleaseDoOnce = false;
                        break;
                    case 2:
                        Honey(true);
                        Honey_Effect.SetActive(true);
                        pleaseDoOnce = false;
                        break;
                    case 3:
                        Shield(true);
                        Shield_Effect.SetActive(true);
                        Debug.Log("am called");
                        pleaseDoOnce = false;
                        break;
                    case 4:
                        Magnet(true);
                        Magnet_Effect.SetActive(true);
                        pleaseDoOnce = false;
                        break;
                    default:
                        break;
                }
            }
        }
        else
            pleaseDoOnce = true;

        if (Raygun_Effect.activeInHierarchy)
        {
            RaygunTimer -= Time.deltaTime;
            if (RaygunTimer < 0)
            {
                Raygun_Effect.SetActive(false);
            }
        }
        else
            RaygunTimer = 2;

        if (Fence_Effect.activeInHierarchy)
        {
            FenceTimer -= Time.deltaTime;
            if (FenceTimer < 0)
            {
                Fence_Effect.SetActive(false);
            }
        }
        else
            FenceTimer = 2;

        if (Honey_Effect.activeInHierarchy)
        {
            HoneyTimer -= Time.deltaTime;
            if (HoneyTimer < 0)
            {
                Honey_Effect.SetActive(false);
            }
        }
        else
            HoneyTimer = 2;

        if (Shield_Effect.activeInHierarchy)
        {
            ShieldETimer -= Time.deltaTime;
            if (ShieldETimer < 0)
            {
                Debug.Log("Setting shiled to false you plebian, srry Ican't spill");
                Shield_Effect.SetActive(false);
            }
        }
        else
            ShieldETimer = 2;

        if (Magnet_Effect.activeInHierarchy)
        {
            MagnetTimer -= Time.deltaTime;
            if (MagnetTimer < 0)
            {
                Magnet_Effect.SetActive(false);
            }
        }
        else
            MagnetTimer = 2;
    }

    private void LateUpdate()
    {
        //sprintBar.transform.rotation = new Quaternion(0, 0, 0, 1);
        canvas.transform.rotation = new Quaternion(0, 0, 0, 1);
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
            Time.timeScale = 1f;
            menuPanel.SetActive(false);
        }
        else if (menuPanel.activeInHierarchy == false)
        {
            Time.timeScale = 0f;
            menuPanel.SetActive(true);

            //EventSystem eventSystem = EventSystem.current;
            //eventSystem.SetSelectedGameObject(pauseButtonFirst);
        }
    }
}
