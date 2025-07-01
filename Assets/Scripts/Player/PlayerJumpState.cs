using UnityEngine;

public class PlayerJumpState : CharacterState
{
    public PlayerJumpState(StateMachine stateMachine, CharacterBaseController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetTrigger("JumpTrigger");
        _controller.rb.AddForce(new Vector2(_controller.rb.linearVelocity.x, _controller.stats.GetStat(StatType.JumpForce)),ForceMode2D.Impulse);
        AudioManager.Instance.PlayJump();
    }

    public override void Update()
    {
        if (_controller.rb.linearVelocity.y < 0f)
        {
            _stateMachine.ChangeState(new PlayerFallState(_stateMachine, _controller));
        }
    }

    public override void FixedUpdate()
    {
        float speed = _controller.stats.GetStat(StatType.Speed);
        _controller.rb.linearVelocity = new Vector2(speed * _controller.direction, _controller.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        //Nothing
    }
}
