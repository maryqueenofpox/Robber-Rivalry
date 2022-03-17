using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb { get; set; }
    public Vector2 movementInput { get; set; }

    [Header("Movement Values")]
    public float moveSpeed = 50.0f;
    [SerializeField] public float originalmoveSpeed = 250.0f;
    [SerializeField] float dashForce = 10f;

    public float dashDuration = 2f;
    public bool isDashing { get; set; }
    public float maxDashTime { get; set; }

    [SerializeField] float durationToIncreaseBy = 0.1f;

    PlayerControls playerControlsScript;
    PlayerAnimations playerAnimationsScript;

    public bool canDash { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        rb = GetComponent<Rigidbody>();
        maxDashTime = dashDuration;
        playerControlsScript = GetComponent<PlayerControls>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();

        canDash = true;
    }

    private void Update()
    {
        if (dashDuration <= 0)
        {
            playerAnimationsScript.IsDashingAnimation(false);
        }

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
            {
                dashDuration = maxDashTime;
                canDash = true;

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerControlsScript.isStunned) // this can potentially be changed
        {
            if (isDashing)
            {
                rb.velocity = transform.TransformDirection(Vector3.forward) * moveSpeed * dashForce * Time.deltaTime;
                playerAnimationsScript.IsDashingAnimation(true);
            }
            else
            {
                rb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;
                playerAnimationsScript.IsDashingAnimation(false);
            }
                
        }
        else // keeping this if statement for the variable below
            rb.velocity = Vector3.zero;


        if (rb.velocity != Vector3.zero)
        {
            playerAnimationsScript.IdleAnimation(false);
            playerAnimationsScript.IsMovingAnimation(true);
        }
        else
        {
            playerAnimationsScript.IsMovingAnimation(false);
            playerAnimationsScript.IdleAnimation(true);
        }

        if (!(rb.velocity == Vector3.zero) && !isDashing)
            transform.rotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y));
    }

    /*/
     Trigger for player walking into honey pile on floor; speed should be reduced and then return to normal after they exit trigger
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Honey"))
        {
            moveSpeed = 50.0f;
        }

        if (other.gameObject.CompareTag("HoneyExit"))
        {
            moveSpeed = originalmoveSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Honey"))
        {
            moveSpeed = originalmoveSpeed;
        }

        if (other.gameObject.CompareTag("HoneyExit"))
        {
            moveSpeed = originalmoveSpeed;
        }
    }
}
