using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private CharacterState currentState;

    public void ChangeState(CharacterState newState)
    {
        currentState?.Exit();         
        currentState = newState;
        currentState.Enter();         
    }

    public void Update()
    {
        currentState?.Update();      
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
