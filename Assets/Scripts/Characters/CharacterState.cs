public abstract class CharacterState
{
    protected StateMachine _stateMachine;
    protected CharacterBaseController _controller;

    public CharacterState(StateMachine stateMachine, CharacterBaseController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}
