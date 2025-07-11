using UnityEngine;

public class PlayerRunState : CharacterState
{
    public PlayerRunState(StateMachine stateMachine, CharacterBaseController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetBool("IsRunning", true);
    }

    public override void Update()
    {
        if (!_controller.isGrounded)
        {
            _stateMachine.ChangeState(new PlayerFallState(_stateMachine, _controller));
            return;
        }
        else
        {
            if (_controller.attackRequested)
            {
                _stateMachine.ChangeState(new PlayerAttack1State(_stateMachine, _controller));
                return;
            }

            if (_controller.rollRequested)
            {
                _stateMachine.ChangeState(new PlayerRollState(_stateMachine, _controller));
                return;
            }

            if (!_controller.isRunning)
            {
                _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, _controller));
            }
            else if (_controller.jumpRequested)
            {
                _stateMachine.ChangeState(new PlayerJumpState(_stateMachine, _controller));
            }
        }
    }

    public override void FixedUpdate()
    {
        _controller.rb.linearVelocity = new Vector2(_controller.stats.GetStat(StatType.Speed) * _controller.direction, _controller.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        _controller.anim.SetBool("IsRunning", false);
    }
}
