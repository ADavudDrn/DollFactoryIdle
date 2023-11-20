using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvent
{
    [AddComponentMenu("ScriptableEvent/Game Event Listener")]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent Event;
        [SerializeField] private bool UseDelay;
        [SerializeField] private float FloatDelay;
        [SerializeField] private UnityEvent Response;
        
        public void OnEventRaised()
        {
            GiveResponse();
        }

        private void GiveResponse()
        {
            if (!UseDelay)
            {
                Response?.Invoke();
                return;
            }
            
            var delay =  FloatDelay;
            DOVirtual.DelayedCall(delay, () => Response?.Invoke());
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    }
}