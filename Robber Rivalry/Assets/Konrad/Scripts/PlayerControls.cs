using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    bool grounded;
    public float jumpForce = 1.0f, moveSpeed = 50.0f;
    Vector2 movementInput;
    float sprintTime = 10f;
    float maxSprintTime;
    bool isSprinting = false;
    float sprintForce = 2f;
    float timeUntilRecharge = 2f;
    float originalRechargeTime;

    [SerializeField] GameObject wetFloorSign;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSprintTime = sprintTime;
        originalRechargeTime = timeUntilRecharge;
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
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (grounded)
            transform.Translate(new Vector3(0, jumpForce, 0) * Time.deltaTime);
    }

    public void Ability1(InputAction.CallbackContext ctx)
    {
        if(GameObject.Find("PowerUp").GetComponent<PowerUp>().canUseAbility)
        {
            Instantiate(wetFloorSign, transform.position+(transform.forward*2), Quaternion.identity);
        }
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isSprinting = true;
        else if (ctx.canceled)
            isSprinting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }
}