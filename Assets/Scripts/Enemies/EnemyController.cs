using UnityEngine;

public class EnemyController : CharacterBaseController
{
    [Header("Jump Settings")]
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float groundDetection;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameEvent onPlayerEnterArea;
    private StateMachine _stateMachine;
    private Transform target;

    //States
    public float direction;
    public bool isGrounded;
    public bool isRunning;
    public bool isPatrolling;
    public bool isChasing;
    public bool attackRequested;
    public bool hitRequested;
    public bool deathRequested;

    protected override void Awake()
    {
        base.Awake();
        target = null;
        _stateMachine = GetComponent<StateMachine>();
    }

    void Start()
    {
        _stateMachine.ChangeState(new EnemyIdleState(_stateMachine, this));
    }

    void Update()
    {
        _stateMachine.Update();
        attackRequested = false;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundDetection, groundMask);
        _stateMachine.FixedUpdate();
    }
}
