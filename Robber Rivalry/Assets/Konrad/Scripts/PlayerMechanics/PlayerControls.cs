using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    SlapMechanic slapMechanicScript;
    GemMechanic gemMechanicScript;
    BashMechanic bashMechanicScript;
    PlayerUI playerUIScript;
    PlayerMovement playerMovementScript;
    PlayerAbilities playerAbilitiesScript;
    ForceField forceFieldScript;
    PlayerAnimations playerAnimationsScript;

    [SerializeField] float stunDuration = 1f;
    float originalStunDuration;
    public bool isStunned;
    
    public bool vulnerable { get; set; }
    [SerializeField] float vulnerableTimer = 1f;
    float originalVulnerableTimer;

    private void Start()
    {
        GetStartingComponents();

        originalStunDuration = stunDuration;
        originalVulnerableTimer = vulnerableTimer;
        vulnerable = true;
    }

    private void Update()
    {
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
            if (ctx.performed)
                playerMovementScript.isDashing = true;
            else
                playerMovementScript.isDashing = false;
        else
            return;
    }

    public void PickUpGem(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            gemMechanicScript.PickUpGem();
        else
            return;
    }

    public void DropGem(InputAction.CallbackContext ctx)
    {
        gemMechanicScript.DropGem();
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
            slapMechanicScript.DoTheSlap();
        else
            return;
    }

    void GetStartingComponents()
    {
        slapMechanicScript = GetComponent<SlapMechanic>();
        gemMechanicScript = GetComponent<GemMechanic>();
        bashMechanicScript = GetComponent<BashMechanic>();
        playerUIScript = GetComponent<PlayerUI>();
        playerMovementScript = GetComponent<PlayerMovement>();
        playerAbilitiesScript = GetComponent<PlayerAbilities>();
        forceFieldScript = GetComponent<ForceField>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
    }
}