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
    [SerializeField] float dashCoolDown = 2f;
    float originalDashCoolDown;
    bool isDashing;

    [HideInInspector] public bool isCarryingGem = false;
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

    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canUseAbility = false;
        originalSlapCooldown = slapCooldown;
        originalTimeUntilScoreIncrease = timeUntilScoreIncrease;
        //Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency); //<< For controller vibration

        menuPanel.SetActive(false);
        isPlayer = false;
        powerUpUI.SetActive(false);
        originalDashCoolDown = dashCoolDown;
        isDashing = false;
        originalStunDuration = stunDuration;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * reach, Color.red);
        
        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            // play animation
            if (stunDuration <= 0)
            {
                // stop animation
                isStunned = false;
                stunDuration = originalStunDuration;
                // enable idle animation
            }
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
            dashCoolDown -= Time.deltaTime;
            
            if (dashCoolDown <= 0)
            {
                isDashing = false;
                dashCoolDown = originalDashCoolDown;
            }
        }
    }

    private void FixedUpdate()
    {
        /// CHANGE SPRINT TO DASH!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// doneish.

        rb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;

        /// if velocity is 0
        /// set the animation to play idle
        /// peace

        if (!(rb.velocity == Vector3.zero))
            transform.rotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y));

        if (Gamepad.current.rightStick.IsActuated())
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
        if (canUseAbility)
        {
            SpawnObject();
            canUseAbility = false;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!isDashing)
        {
            rb.AddForce(transform.TransformDirection(Vector3.forward * dashForce));
            isDashing = true;
        }
        
    }

    public void PickUpGem(InputAction.CallbackContext ctx)
    {
        if (!isCarryingGem)
        {
            if (isGem)
            {
                gemTransform.parent = transform;
                transform.GetChild(1).localPosition = new Vector3(0, 3.5f, 0);
                isCarryingGem = true;
                isGem = false;
            }
        }
    }

    public void DropGem(InputAction.CallbackContext ctx)
    {
        if (isCarryingGem)
        {
            transform.GetChild(1).transform.localPosition = transform.TransformDirection(Vector3.forward * 1);
            transform.GetChild(1).transform.parent = null;
            isCarryingGem = false;
        }
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        if (canSlap)
        {
            if (isPlayer)
            {
                controls.rb.AddForce(transform.forward * slapFoce);
                controls.isStunned = true;

                if (controls.isCarryingGem)
                {
                    controls.transform.GetChild(1).transform.localPosition = controls.transform.TransformDirection(-Vector3.forward * 2);
                    controls.transform.GetChild(1).transform.parent = null;
                    controls.isCarryingGem = false;
                }

                canSlap = false;
            }
        }
    }

    void SpawnObject()
    {
        Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x, 2.44f, transform.position.z + transform.forward.z), wetFloorSign.transform.rotation);
        AstarPath.active.Scan();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
            canUseAbility = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controls = other.GetComponent<PlayerControls>();
            isPlayer = true;
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