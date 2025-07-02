using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public CharacterState currentState;

    public void ChangeState(CharacterState newState)
    {
        Debug.Log(currentState);
        currentState?.Exit();         
        currentState = newState;
        Debug.Log(currentState);
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
