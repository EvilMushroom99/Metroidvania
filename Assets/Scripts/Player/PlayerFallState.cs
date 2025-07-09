using UnityEngine;

public class PlayerFallState : CharacterState<PlayerController>
{
    public PlayerFallState(StateMachine stateMachine, PlayerController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetBool("IsFalling", true);
    }

    public override void Update()
    {
        if (_controller.isGrounded)
        {
            _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, _controller));
        }
    }

    public override void FixedUpdate()
    {
        float speed = _controller.stats.GetStat(StatType.Speed);
        _controller.rb.linearVelocity = new Vector2(speed * _controller.direction, _controller.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        _controller.anim.SetBool("IsFalling", false);
    }
}
