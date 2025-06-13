using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed;
    public float speedMultiplier;

    [Header("Jump Settings")]
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private float jumpDetection;
    public float jumpForce;
    [SerializeField] private LayerMask layerMask;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;

    private PlayerInput playerInput;

    bool isGrounded;
    bool isRunning;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += Move;
        playerInput.actions["Move"].canceled += Move;
        playerInput.actions["Jump"].performed += Jump;
        playerInput.actions["Inventory"].performed += Inventory;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        float axisValue = ctx.ReadValue<float>();
        if (ctx.performed)
        {
            isRunning = true;
            speed = axisValue;
            spriteRenderer.flipX = !(speed > 0f);

        }
        else if (ctx.canceled)
        {
            isRunning = false;
            speed = 0f;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce), ForceMode2D.Impulse);
            AudioManager.Instance.PlayJump();
        }
    }

    private void Inventory(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            PlayerInventory.Instance.OpenInventory();
            AudioManager.Instance.PlayUI();
        }
    }


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, jumpDetection, layerMask);
        rb.linearVelocity = new Vector2(speedMultiplier * speed, rb.linearVelocity.y);
    }

    private void LateUpdate()
    {
        if (isGrounded)
        {
            if (isRunning) anim.Play("Running");
            else anim.Play("Idle");
        }
        else
        {
            anim.Play("Jump");
        }
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].started -= Move;
        playerInput.actions["Move"].canceled -= Move;
        playerInput.actions["Jump"].performed -= Jump;
        playerInput.actions["Inventory"].performed -= Inventory;
    }
}