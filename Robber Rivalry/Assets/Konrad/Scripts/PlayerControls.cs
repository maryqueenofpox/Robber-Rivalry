using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movementInput;
    Vector2 rotationInput;

    [Header("Movement Values")]
    public float moveSpeed = 50.0f;
    [SerializeField] float dashForce = 10f;

    [Header("Abilities")]
    [SerializeField] GameObject wetFloorSign;
    [SerializeField] float reach = 1.5f;
    [SerializeField] float slapFoce = 5000f;
    [SerializeField] float slapCooldown = 3f;
    [SerializeField] float dashDuration = 2f;
    [SerializeField] bool isDashing;
    float maxDashTime;
    [SerializeField] float bashForce = 2000000f;

    public bool isCarryingGem = false;
    float originalSlapCooldown;
    bool canUseAbility;
    bool canSlap = true;

    [Header("Loot Grabber Script")]
    public LootGrabber lootGrabber;

    [Header("Gem Score")]
    [SerializeField] float gemPointIncrease;
    public float timeUntilScoreIncrease = 4.0f;
    float originalTimeUntilScoreIncrease;
    Transform gemTransform;

    [Header("UI")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject powerUpUI;

    bool isPlayer;
    bool isGem;
    PlayerControls controls;

    bool isStunned;
    [SerializeField] float stunDuration = 1f;
    float originalStunDuration;

    Transform gemChild;
    Animator anim;

    [SerializeField]
    float timeToSetSlapToFalse = 1f;
    float originalSlapToFalse;
    [SerializeField]
    float durationToIncreaseBy = 0.1f;

    bool doTheSlap = false;
    [SerializeField] AudioSource powerUpAudio;

    private void Start()
    {
        originalSlapToFalse = timeToSetSlapToFalse;
        rb = GetComponent<Rigidbody>();
        canUseAbility = false;
        originalSlapCooldown = slapCooldown;
        originalTimeUntilScoreIncrease = timeUntilScoreIncrease;

        menuPanel.SetActive(false);
        isPlayer = false;
        powerUpUI.SetActive(false);
        isDashing = false;
        originalStunDuration = stunDuration;
        anim = GetComponent<Animator>();
        maxDashTime = dashDuration;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * reach, Color.red);

        if (anim.GetBool("isSlapping") == true)
        {
            timeToSetSlapToFalse -= Time.deltaTime;
            if (timeToSetSlapToFalse <= 0f)
            {
                timeToSetSlapToFalse = originalSlapToFalse;
                anim.SetBool("isSlapping", false);
            }
       
        }

        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            anim.SetBool("gotSlapped", true);
            if (stunDuration <= 0)
            {
                anim.SetBool("gotSlapped", false);
                isStunned = false;
                stunDuration = originalStunDuration;
                anim.SetBool("idle", true);
            }
        }

        if (dashDuration <= 0)
        {
            anim.SetBool("isDashing", false);
        }

        if (!canSlap)
        {
            slapCooldown -= Time.deltaTime;
        }

        if (slapCooldown <= 0.0f)
        {
            canSlap = true;
            slapCooldown = originalSlapCooldown;
        }

        if (isCarryingGem)
        {
            if (transform.childCount < 2)
                isCarryingGem = false;
            else
            {
                timeUntilScoreIncrease -= Time.deltaTime;
                if (timeUntilScoreIncrease <= 0.0f)
                {
                    lootGrabber.loot += gemPointIncrease;
                    lootGrabber.score.text = lootGrabber.loot.ToString();
                    timeUntilScoreIncrease = originalTimeUntilScoreIncrease;
                }
            }
        }

        if (canUseAbility)
            powerUpUI.SetActive(true);
        else
            powerUpUI.SetActive(false);

        if (isDashing)
        {
            dashDuration -= Time.deltaTime;
            
            if (dashDuration <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            dashDuration += durationToIncreaseBy * Time.deltaTime;
            if (dashDuration >= maxDashTime)
                dashDuration = maxDashTime;
        }

        if (doTheSlap)
            DoTheSlap();

        Debug.Log("Gem Status: " + isCarryingGem);
    }

    private void FixedUpdate()
    {
        if (!isStunned)
        {
            if (isDashing)
            {
                rb.velocity = transform.TransformDirection(Vector3.forward) * moveSpeed * dashForce * Time.deltaTime;
                // anim is dashing
            }
            else
                rb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;
        }
        else
            rb.velocity = Vector3.zero;
        

        if (rb.velocity != Vector3.zero)
        {
            anim.SetBool("idle", false);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("idle", true);
        }

        if (!(rb.velocity == Vector3.zero) && !isDashing)
            transform.rotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y));

        if (Gamepad.current.rightStick.IsActuated() && !isDashing)
            transform.rotation = Quaternion.LookRotation(new Vector3(rotationInput.x, 0, rotationInput.y));
    }

    public void Rotation(InputAction.CallbackContext ctx)
    {
        rotationInput = ctx.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OpenOrCloseMenuPanel(InputAction.CallbackContext ctx)
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

    public void Ability1(InputAction.CallbackContext ctx)
    {
        if (canUseAbility && !isStunned)
        {
            SpawnObject();
            canUseAbility = false;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!isStunned)
        {
            if (ctx.performed)
            {
                anim.SetBool("isDashing", true);
                isDashing = true;
            }                 
            else               
            {
                anim.SetBool("isDashing", false);
                isDashing = false;
            }
        }
    }

    public void PickUpGem(InputAction.CallbackContext ctx)
    {
        if (!isCarryingGem && !isStunned)
        {
            if (isGem)
            {
                gemTransform.parent = transform;
                gemChild = transform.Find("Gem");
                gemChild.localPosition = new Vector3(0, 2.5f, 0);
                isCarryingGem = true;
                isGem = false;
            }
        }
    }

    public void DropGem(InputAction.CallbackContext ctx)
    {
        if (isCarryingGem)
        {
            gemChild.localPosition = transform.TransformDirection(Vector3.forward * 1);
            gemChild.parent = null;
            isCarryingGem = false;
        }
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        doTheSlap = true;
    }

    void DoTheSlap()
    {
        if (canSlap && !isStunned)
        {
            anim.SetBool("isSlapping", true);
            if (timeToSetSlapToFalse <= 0.1f)
            {
                if (isPlayer)
                {
                    controls.rb.AddForce(transform.forward * slapFoce);
                    controls.isStunned = true;

                    if (controls.isCarryingGem)
                    {
                        controls.transform.Find("Gem").transform.localPosition = controls.transform.TransformDirection(-Vector3.forward * 2);
                        controls.transform.Find("Gem").transform.parent = null;
                        controls.isCarryingGem = false;
                    }

                    canSlap = false;
                    doTheSlap = false;
                }
                doTheSlap = false;
            }
        }
    }

    void SpawnObject()
    {
        Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x, 0f, transform.position.z + transform.forward.z), transform.rotation); //wetFloorSign.transform.rotation);
        AstarPath.active.Scan();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
        {
            powerUpAudio.Play();
            canUseAbility = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controls = other.GetComponent<PlayerControls>();
            isPlayer = true;

            if (isDashing)
            {
                int randomDirection = Random.Range(0, 2);
                switch (randomDirection)
                {
                    case 0:
                        controls.rb.AddForce(-transform.right * bashForce);
                        break;
                    case 1:
                        controls.rb.AddForce(transform.right * bashForce);
                        break;
                    default:
                        Debug.Log("Error, random direction error in trigger collider.");
                        break;
                }
            }
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            gemTransform = other.GetComponent<Transform>();
            isGem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayer = false;
        isGem = false;
    }
}