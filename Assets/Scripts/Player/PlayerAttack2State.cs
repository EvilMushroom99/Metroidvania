using UnityEngine;

public class PlayerAttack2State : CharacterState<PlayerController>
{
    public PlayerAttack2State(StateMachine stateMachine, PlayerController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetTrigger("Attack2Trigger");
    }

    public override void Update()
    {
        var info = _controller.anim.GetCurrentAnimatorStateInfo(0);

        if (_controller.rollRequested && _controller.isGrounded)
        {
            _stateMachine.ChangeState(new PlayerRollState(_stateMachine, _controller));
            return;
        }

        if (info.normalizedTime >= 1f)
        {
            if (_controller.isGrounded)
            {
                if (_controller.isRunning)
                {
                    _stateMachine.ChangeState(new PlayerRunState(_stateMachine, _controller));
                }
                else
                {
                    _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, _controller));
                }
            }
            else _stateMachine.ChangeState(new PlayerFallState(_stateMachine, _controller));
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
