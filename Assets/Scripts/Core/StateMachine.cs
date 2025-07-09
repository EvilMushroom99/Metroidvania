using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public CharacterState currentState;

    public void ChangeState(CharacterState newState)
    {
        Debug.Log("cerrando: " + currentState);
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        Debug.Log("entrando: " + currentState);
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
