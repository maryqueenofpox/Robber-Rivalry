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

    [SerializeField] float spawningDuration = 1f;
    float originalSpawningDuration;
    [HideInInspector] public bool canDoStuff;

    //public bool slapOncePleaseForTheLoveOfGod;

    private void Start()
    {
        GetStartingComponents();

        originalStunDuration = stunDuration;
        originalVulnerableTimer = vulnerableTimer;
        vulnerable = true;
        gotShot = false;

        originalSpawningDuration = spawningDuration;
        canDoStuff = true;

        //GetComponent<MeshRenderer>().material.e
    }

    private void Update()
    {
        if (!canDoStuff)
        {
            spawningDuration -= Time.deltaTime;
            playerMovementScript.movementInput = new Vector2(0, 0);

            foreach(SkinnedMeshRenderer skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                skinnedMeshRenderer.enabled = false;
            }

            if (spawningDuration <= 0)
            {
                canDoStuff = true;
                foreach (SkinnedMeshRenderer skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    skinnedMeshRenderer.enabled = true;
                }
                spawningDuration = originalSpawningDuration;
            }
        }

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
        if (!canDoStuff)
        {
            return;
        }
        else
        {
            if (!isStunned)
                playerMovementScript.movementInput = ctx.ReadValue<Vector2>();
            else
                return;
        }
    }

    public void OpenOrCloseMenuPanel(InputAction.CallbackContext ctx)
    {
        playerUIScript.DoShitToMenu();
    }

    public void Ability(InputAction.CallbackContext ctx)
    {
        if (!canDoStuff)
        {
            return;
        }
        else
        {
            if (!isStunned)
                playerAbilitiesScript.Ability();
            else
                return;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!canDoStuff)
        {
            return;
        }
        else
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
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        if (!canDoStuff)
        {
            return;
        }
        else
        {
            if (ctx.performed)
            {
                if (!isStunned && !slapMechanicScript.doingSlap)
                {
                    slapMechanicScript.DoTheSlap();
                }
                else
                    return;
            }
        }
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