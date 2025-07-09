using UnityEngine;

public class PlayerAttack1State : CharacterState
{
    private bool _queuedAttack2 = false;

    public PlayerAttack1State(StateMachine stateMachine, CharacterBaseController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        _queuedAttack2 = false;
        _controller.anim.SetTrigger("Attack1Trigger");
    }

    public override void Update()
    {
        var info = _controller.anim.GetCurrentAnimatorStateInfo(0);

        if (_controller.attackRequested)
            _queuedAttack2 = true;

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

        if (info.normalizedTime >= 0.75f && _controller.isGrounded)
        {
            if (_queuedAttack2)
            {
                _stateMachine.ChangeState(new PlayerAttack2State(_stateMachine, _controller));
                return;
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
