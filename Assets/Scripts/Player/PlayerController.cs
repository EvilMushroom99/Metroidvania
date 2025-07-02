using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBaseController
{
    [Header("Roll Cooldown")]
    [SerializeField] private float rollCooldown = 1.0f;
    private float _rollTimer = 0f;

    [Header("Jump Settings")]
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private float jumpDetection;
    [SerializeField] private LayerMask layerMask;

    public GameEvent onInventoryOpen;

    private StateMachine _stateMachine;
    private PlayerInput _playerInput;

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();

        _stateMachine = GetComponent<StateMachine>();
    }

    private void OnEnable()
    {
        _playerInput.actions["Move"].performed += Move;
        _playerInput.actions["Move"].canceled += Move;
        _playerInput.actions["Jump"].performed += Jump;
        _playerInput.actions["Inventory"].performed += Inventory;
        _playerInput.actions["Attack"].performed += Attack;
        _playerInput.actions["Roll"].performed += Roll;
    }

    void Start()
    {
        _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, this));
    }

    void Update()
    {
        _stateMachine.Update();

        if (_rollTimer > 0f)
        {
            rollRequested = false;
            _rollTimer -= Time.deltaTime;
        }

        jumpRequested = false;
        attackRequested = false;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, jumpDetection, layerMask);
        _stateMachine.FixedUpdate();
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
        if (ctx.performed) jumpRequested = true;
    }

    private void Inventory(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onInventoryOpen.Raise();
            AudioManager.Instance.PlayUI();
        }
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) attackRequested = true;
    }

    private void Roll(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _rollTimer <= 0f)
        {
            rollRequested = true;
            _rollTimer = rollCooldown;
        }
    }

    private void OnDisable()
    {
        _playerInput.actions["Move"].started -= Move;
        _playerInput.actions["Move"].canceled -= Move;
        _playerInput.actions["Jump"].performed -= Jump;
        _playerInput.actions["Inventory"].performed -= Inventory;
        _playerInput.actions["Attack"].performed -= Attack;
        _playerInput.actions["Roll"].performed -= Roll;
    }
}