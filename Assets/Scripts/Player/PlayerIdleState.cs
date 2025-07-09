using UnityEngine;

public class PlayerIdleState : CharacterState<PlayerController>
{
    public PlayerIdleState(StateMachine stateMachine, PlayerController controller) : base(stateMachine, controller){ }

    public override void Enter()
    {
        _controller.anim.SetBool("IsRunning", false);
    }

    public override void Update()
    {
        if (_controller.isGrounded)
        {
            if (_controller.attackRequested)
            {
                _stateMachine.ChangeState(new PlayerAttack1State(_stateMachine, _controller));
                return;
            }

            if (_controller.jumpRequested)
            {
                _stateMachine.ChangeState(new PlayerJumpState(_stateMachine, _controller));
                return;
            }

            if (_controller.rollRequested)
            {
                _stateMachine.ChangeState(new PlayerRollState(_stateMachine, _controller));
                return;
            }

            if (_controller.isRunning)
            {
                _stateMachine.ChangeState(new PlayerRunState(_stateMachine, _controller));
            }
        }
        else
        {
            _stateMachine.ChangeState(new PlayerFallState(_stateMachine, _controller));
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
