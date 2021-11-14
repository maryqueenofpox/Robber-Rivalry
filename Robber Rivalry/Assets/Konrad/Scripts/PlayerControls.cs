using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] int reach = 10;
    bool canUseAbility;
    [HideInInspector] public bool isCarryingGem = false;

    [SerializeField] float slapFoce = 5000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSprintTime = sprintTime;
        originalRechargeTime = timeUntilRecharge;
        canUseAbility = false;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
    }

    private void FixedUpdate()
    {
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
        RaycastHit hit;
        
        if (!isCarryingGem)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, reach) && hit.transform.CompareTag("Gem"))
            {
                hit.transform.parent = transform;
                transform.GetChild(0).localPosition = new Vector3(0, 3.5f, 0);
                isCarryingGem = true;
            }
        }
    }

    public void DropGem(InputAction.CallbackContext ctx)
    {
        if (isCarryingGem)
        {
            transform.GetChild(0).transform.localPosition = transform.TransformDirection(Vector3.forward * 1);
            transform.GetChild(0).transform.parent = null;
            isCarryingGem = false;
        }
    }

    public void Slap(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, reach)) //&& hit.transform.CompareTag("Player"))
        {
            GameObject objectHit = hit.transform.gameObject;
            
            if (objectHit.CompareTag("Player"))
            {
                PlayerControls controls = objectHit.GetComponent<PlayerControls>();
                controls.rb.AddForce(transform.forward * slapFoce);

                if (controls.isCarryingGem)
                {
                    objectHit.transform.GetChild(0).transform.localPosition = objectHit.transform.TransformDirection(-Vector3.forward * 2);
                    objectHit.transform.GetChild(0).transform.parent = null;
                    controls.isCarryingGem = false;
                }
            }
        }
    }

    void SpawnObject()
    {
        Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x, 2.44f, transform.position.z + transform.forward.z), wetFloorSign.transform.rotation);
    }

    // Yes, I could have used the below green code
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
            canUseAbility = true;
    }
}