using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private float jumpDetection;
    [SerializeField] private LayerMask layerMask;

    public GameEvent onInventoryOpen;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private CharacterStats stats;
    private float direction;

    private PlayerInput playerInput;

    bool isGrounded;
    bool isRunning;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
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
            direction = axisValue;
            spriteRenderer.flipX = !(direction > 0f);

        }
        else if (ctx.canceled)
        {
            isRunning = false;
            direction = 0f;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, stats.GetStat(StatType.JumpForce)), ForceMode2D.Impulse);
            AudioManager.Instance.PlayJump();
        }
    }

    private void Inventory(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onInventoryOpen.Raise();
            AudioManager.Instance.PlayUI();
        }
    }


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, jumpDetection, layerMask);
        rb.linearVelocity = new Vector2(stats.GetStat(StatType.Speed) * direction, rb.linearVelocity.y);
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