using Player;
using Reference;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Unlockable
{
    public class UnlockableArea : MonoBehaviour
    {
        [SerializeField] private UnlockableSO Data;
        [SerializeField] private IntRef Money;
        [SerializeField] private float BuyWaitTime = 2f;

        [SerializeField] private TextMeshPro CostText;

        [SerializeField] private UnityEvent UnlockEvent;

        private bool _isPlayerInTrigger;
        private float _elapsedTime;


        private void OnEnable()
        {
            Data.Load();
            if(Data.IsUnlocked)
                Unlock();
        }

        private void Awake()
        {
            CostText.SetText("$" + Data.Cost);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent(out PlayerStacker playerStacker))
                return;

            _isPlayerInTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.TryGetComponent(out PlayerStacker playerStacker))
                return;

            _isPlayerInTrigger = false;
            _elapsedTime = 0;
        }

        private void Update()
        {
            if(!_isPlayerInTrigger)
                return;

            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= BuyWaitTime)
                TryUnlock();
        }

        public void TryUnlock()
        {
            if(Data.IsUnlocked)
                return;
            
            if(Money.Value < Data.Cost)
                return;

            Money.Value -= Data.Cost;
            Unlock();
        }

        private void Unlock()
        {
            Data.Unlock();
            UnlockEvent?.Invoke();
        }
    }
}