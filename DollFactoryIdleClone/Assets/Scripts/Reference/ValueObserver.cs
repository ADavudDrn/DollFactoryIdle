using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Reference
{
    public class ValueObserver : MonoBehaviour
    {
        [SerializeField, Required] private RefValue Reference;
        [SerializeField] private bool WaitUntilStart = true;

        [SerializeField, Space] private UnityEvent OnReferenceChanged;
        
        private bool _isActive;

        private void Awake()
        {
            if(WaitUntilStart)
                return;
            _isActive = true;
        }

        private void OnEnable()
        {
            Reference.OnValueChanged += ResponseToValueChange;
        }

        private void OnDisable()
        {
            Reference.OnValueChanged -= ResponseToValueChange;
        }

        private void Start()
        {
            if(!WaitUntilStart)
                return;
            StartCoroutine(WaitForNextFrameToActivate());
        }

        private IEnumerator WaitForNextFrameToActivate()
        {
            yield return new WaitForEndOfFrame();
            _isActive = true;
        }

        private void ResponseToValueChange()
        {
            if(!_isActive)
                return;
            
            OnReferenceChanged?.Invoke();
        }
    }
}