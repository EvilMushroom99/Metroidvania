using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBaseController
{
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
    }

    void Start()
    {
        _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, this));
    }

    void Update()
    {
        _stateMachine.Update();
        jumpRequested = false;
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

    private void OnDisable()
    {
        _playerInput.actions["Move"].started -= Move;
        _playerInput.actions["Move"].canceled -= Move;
        _playerInput.actions["Jump"].performed -= Jump;
        _playerInput.actions["Inventory"].performed -= Inventory;
    }
}