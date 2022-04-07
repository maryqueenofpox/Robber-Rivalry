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
    float originalStunDuration;
    public bool isStunned;
    
    public bool vulnerable { get; set; }
    [SerializeField] float vulnerableTimer = 1f;
    float originalVulnerableTimer;

    public bool gotShot { get; set; }

    Vector2 rot;

    private void Start()
    {
        GetStartingComponents();

        originalStunDuration = stunDuration;
        originalVulnerableTimer = vulnerableTimer;
        vulnerable = true;
        gotShot = false;
    }

    private void Update()
    {
        if (gotShot)
        {
            stunDuration = gotShotStunDuration;
            gotShot = false;
        }


        if (isStunned && vulnerable)
        {
            stunDuration -= Time.deltaTime;
            playerAnimationsScript.GotSlappedAnim(true);
            if (stunDuration <= 0)
            {
                playerAnimationsScript.GotSlappedAnim(false);
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

                if (playerMovementScript.dashDuration < playerMovementScript.maxDashTime)
                    playerMovementScript.canDash = false;
            }
        else
            return;
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            slapMechanicScript.doTheSlap=true;
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