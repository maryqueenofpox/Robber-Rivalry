using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    SlapMechanic slapMechanicScript;
    PlayerUI playerUIScript;
    PlayerMovement playerMovementScript;
    PlayerAbilities playerAbilitiesScript;
    PlayerAnimations playerAnimationsScript;

    [SerializeField] float stunDuration = 1f;
    [SerializeField] float gotShotStunDuration = 2f;
    [SerializeField] GameObject DashTrail;
    float originalStunDuration;
    public bool isStunned;
    
    public bool vulnerable { get; set; }
    [SerializeField] float vulnerableTimer = 2f;
    float originalVulnerableTimer;

    public bool gotShot { get; set; }

    public bool slapOncePleaseForTheLoveOfGod;

    private void Start()
    {
        GetStartingComponents();

        originalStunDuration = stunDuration;
        originalVulnerableTimer = vulnerableTimer;
        vulnerable = true;
        gotShot = false;

        slapOncePleaseForTheLoveOfGod = true;
    }

    private void Update()
    {
        if (gotShot)
        {
            stunDuration = gotShotStunDuration;
            gotShot = false;
        }


        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            playerAnimationsScript.GotSlappedAnim(true);
            DashTrail.SetActive(true);
            if (stunDuration <= 0)
            {
                playerAnimationsScript.GotSlappedAnim(false);
                DashTrail.SetActive(false);
                isStunned = false;
                vulnerable = false;
                stunDuration = originalStunDuration;
                playerAnimationsScript.IdleAnimation(true);
            }
        }

        if (!vulnerable)
        {
            vulnerableTimer -= Time.deltaTime;

            if (vulnerableTimer <= 0f)
            {
                vulnerableTimer = originalVulnerableTimer;
                vulnerable = true;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            playerMovementScript.movementInput = ctx.ReadValue<Vector2>();
        else
            return;
    }

    public void OpenOrCloseMenuPanel(InputAction.CallbackContext ctx)
    {
        playerUIScript.DoShitToMenu();
    }

    public void Ability(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            playerAbilitiesScript.Ability();
        else
            return;
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            if (ctx.performed && playerMovementScript.canDash)
                playerMovementScript.isDashing = true;
            else
            {
                playerMovementScript.isDashing = false;
            }
        else
            return;
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        if (!isStunned && slapOncePleaseForTheLoveOfGod)
        {
            slapMechanicScript.doTheSlap = true;
            slapOncePleaseForTheLoveOfGod = false;
        }
        else
            return;
    }

    void GetStartingComponents()
    {
        slapMechanicScript = GetComponent<SlapMechanic>();
        playerUIScript = GetComponent<PlayerUI>();
        playerMovementScript = GetComponent<PlayerMovement>();
        playerAbilitiesScript = GetComponent<PlayerAbilities>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
    }
}