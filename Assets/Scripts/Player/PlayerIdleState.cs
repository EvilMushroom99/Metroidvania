using UnityEngine;

public class PlayerIdleState : CharacterState
{
    public PlayerIdleState(StateMachine stateMachine, CharacterBaseController controller) : base(stateMachine, controller){ }

    public override void Enter()
    {
        _controller.anim.SetBool("IsRunning", false);
    }

    public override void Update()
    {
        if (_controller.jumpRequested && _controller.isGrounded)
        {
            _stateMachine.ChangeState(new PlayerJumpState(_stateMachine, _controller));
            return;
        }

        if (_controller.isRunning)
        {
            _stateMachine.ChangeState(new PlayerRunState(_stateMachine, _controller));
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
