using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableEvent
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "ScriptableEvent/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> _listeners = new List<GameEventListener>();
        private bool _isInitialized;
        
        public void Initialize()
        {
            if (_isInitialized)
                return;
            _listeners = new List<GameEventListener>();
            _isInitialized = true;
        }
        [Button]
        public void Raise()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }
        
        public void RegisterListener(GameEventListener listener)
        {
            _listeners.Add(listener);
        }
        
        public void UnregisterListener(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}