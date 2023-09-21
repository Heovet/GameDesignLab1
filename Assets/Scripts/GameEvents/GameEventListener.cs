using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

public class GameEventListener : MonoBehaviour
{
    public GameEvents gameEvents;

    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvents.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvents.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
