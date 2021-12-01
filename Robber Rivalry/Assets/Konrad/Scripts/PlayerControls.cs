using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 50.0f;
    Vector2 movementInput;
    Vector2 rotationInput;
    [SerializeField]
    float sprintTime = 10f;
    float maxSprintTime;
    bool isSprinting = false;
    [SerializeField]
    float sprintForce = 2f;
    float timeUntilRecharge = 2f;
    float originalRechargeTime;

    [SerializeField] GameObject wetFloorSign;

    [SerializeField] float reach = 1.5f;
    bool canUseAbility;
    [HideInInspector] public bool isCarryingGem = false;

    [SerializeField] float slapFoce = 5000f;

    [SerializeField] float slapCooldown = 3f;
    float originalSlapCooldown;
    bool canSlap = true;

    public LootGrabber lootGrabber;

    public float timeUntilScoreIncrease = 4.0f;
    float originalTimeUntilScoreIncrease;

    public float lowFrequency = 2.0f;
    public float highFrequency = 2.0f;

    [SerializeField] GameObject menuPanel;

    [SerializeField] float vibrationIntensity = 0.3f;
    public bool vibrateController;

    [SerializeField] float rayRadius = 10f;
    [SerializeField] float gemPointIncrease;

    bool isPlayer;
    bool isGem;
    PlayerControls controls;
    Transform gemTransform;
    [SerializeField] GameObject powerUpUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSprintTime = sprintTime;
        originalRechargeTime = timeUntilRecharge;
        canUseAbility = false;
        originalSlapCooldown = slapCooldown;
        originalTimeUntilScoreIncrease = timeUntilScoreIncrease;
        //Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency); //<< For controller vibration

        vibrateController = false;
        menuPanel.SetActive(false);
        isPlayer = false;
        powerUpUI.SetActive(false);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * reach, Color.red);
        

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

        /*
        //Gamepad.current.SetMotorSpeeds(vibrationIntensity, vibrationIntensity);
        if (vibrateController)
            Gamepad.current.SetMotorSpeeds(vibrationIntensity, vibrationIntensity);
        else
            Gamepad.current.SetMotorSpeeds(0, 0);

        Debug.Log("Gamepad ID: " + Gamepad.current.deviceId);
        */
    }

    private void FixedUpdate()
    {
        /// CHANGE SPRINT TO DASH!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        if (isSprinting)
        {
            if (!(sprintTime <= 0f))
            {
                rb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * sprintForce * Time.deltaTime;
                sprintTime -= Time.deltaTime;
            }
            else
            {
                sprintTime = 0f;
                isSprinting = false;
                timeUntilRecharge = originalRechargeTime;
            }
        }
        else if (!isSprinting)
        {
            rb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;

            if (timeUntilRecharge > 0f)
            {
                timeUntilRecharge -= Time.deltaTime;
            }
            else if (timeUntilRecharge <= 0f)
            {
                timeUntilRecharge = 0f;

                sprintTime += Time.deltaTime;

                if (sprintTime >= maxSprintTime)
                    sprintTime = maxSprintTime;
            }
        }
        else
            Debug.Log("Error, is neither sprinting and not sprinting");

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

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isSprinting = true;
        else if (ctx.canceled)
            isSprinting = false;
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