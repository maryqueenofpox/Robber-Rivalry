using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    //bool grounded;
    public float moveSpeed = 50.0f;
    Vector2 movementInput;
    Vector2 rotationInput;
    float sprintTime = 10f;
    float maxSprintTime;
    bool isSprinting = false;
    float sprintForce = 2f;
    float timeUntilRecharge = 2f;
    float originalRechargeTime;

    [SerializeField] GameObject wetFloorSign;

    public bool canUseAbility;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSprintTime = sprintTime;
        originalRechargeTime = timeUntilRecharge;
        canUseAbility = false;
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

    void SpawnObject()
    {
        Instantiate(wetFloorSign, transform.position + transform.forward * 2, Quaternion.identity);
    }

    // Yes, I could have used the below green code
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
            canUseAbility = true;
    }

    // Below might be unnecessary
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }
    */
}