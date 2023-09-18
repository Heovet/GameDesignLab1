using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvents : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();
    
    //Raise event through different methods signature
    public void Raise(Component sender, object data)
    {
        for (int i = listeners.Count -1; i >= 0; i--)
        {
            if (listeners[i] != null) listeners[i].OnEventRaised(sender, data);
        }
    }

    //Manage Listeners
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
