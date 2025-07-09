using UnityEngine;

public class EnemyAttackState : CharacterState<EnemyController>
{
    public EnemyAttackState(StateMachine stateMachine, EnemyController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetTrigger("AttackTrigger");
    }

    public override void Update()
    {
        if (_controller.isGrounded)
        {
            if (_controller.attackRequested)
            {
                _stateMachine.ChangeState(new EnemyAttackState(_stateMachine, _controller));
                return;
            }

            if (_controller.isChasing)
            {
                _stateMachine.ChangeState(new EnemyChaseState(_stateMachine, _controller));
                return;
            }

            if (_controller.isRunning)
            {
                _stateMachine.ChangeState(new EnemyPatrolState(_stateMachine, _controller));
            }
            else
            {
                _stateMachine.ChangeState(new EnemyIdleState(_stateMachine, _controller));
            }
        }
    }

    public override void FixedUpdate()
    {
        //Nothing
    }

    public override void Exit()
    {
        //Nothing
    }
}
