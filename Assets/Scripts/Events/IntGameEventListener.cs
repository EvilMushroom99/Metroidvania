using UnityEngine;
using UnityEngine.Events;

public class IntGameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public IntGameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<int> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(int index)
    {
        Response.Invoke(index);
    }
}

[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }
