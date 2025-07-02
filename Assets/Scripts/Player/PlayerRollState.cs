using UnityEngine;

public class PlayerRollState : CharacterState
{
    private float _timer = 0f;
    private const float _minRollTime = 0.25f;

    public PlayerRollState(StateMachine stateMachine, CharacterBaseController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _controller.anim.SetTrigger("RollTrigger");
        _controller.rb.linearVelocity = new Vector2(_controller.rb.linearVelocity.x + _controller.stats.GetStat(StatType.RollForce) * GetRollDirection(), _controller.rb.linearVelocity.y);
        AudioManager.Instance.PlayJump();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _minRollTime) return;

        var info = _controller.anim.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 1f)
        {
            if (!_controller.isGrounded) _stateMachine.ChangeState(new PlayerFallState(_stateMachine, _controller));
            else if (_controller.isRunning) _stateMachine.ChangeState(new PlayerRunState(_stateMachine, _controller));
            else _stateMachine.ChangeState(new PlayerIdleState(_stateMachine, _controller));
        }
    }

    public override void FixedUpdate() {}

    public override void Exit() {}

    private float GetRollDirection()
    {
        if (_controller.spriteRenderer.flipX) return -1f;
        else return 1f;
    }
}
