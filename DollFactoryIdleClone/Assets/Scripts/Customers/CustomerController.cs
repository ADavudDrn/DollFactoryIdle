using System;
using DG.Tweening;
using Items;
using Lean.Pool;
using LevelElements;
using Player;
using Reference;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Customers
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent Agent;
        [SerializeField] private ShelfListRef ShelfListRef;
        [SerializeField] private RegisterRef Register;
        [SerializeField] private float ShelfStopDistance = 2f;
        [SerializeField] private float LineStopDistance = .1f;
        [SerializeField] private IntRef MoneyRef;
        [SerializeField] private Transform ItemPosition;
        [SerializeField] private TransformRef DespawnPoint;
        [SerializeField] private IntRef CustomerCount;
        [SerializeField] private CustomerAnimationController AnimationController;

        private Shelf _targetShelf;
        private bool _isMovingToShelf;
        private bool _isItemAvaialble;
        private bool _isInLine;
        private bool _isMovingToDespawn;
        private bool _isWaitingForLine;
        private RegisterLinePoint _point;
        private Item _item;

        private float _elapsedTime;
        private float _randomInterval;


        private void OnEnable()
        {
            _targetShelf = null;
            _isMovingToShelf = false;
            _isItemAvaialble = false;
            _isMovingToDespawn = false;
            _isInLine = false;
            _isWaitingForLine = false;
            _point = null;
            _item = null;
            _elapsedTime = 0;
        }

        private void Update()
        {
            if (_isMovingToDespawn)
            {
                if ((transform.position - DespawnPoint.Value.position).magnitude <= LineStopDistance)
                {
                    Agent.isStopped = true;
                    AnimationController.CustomerStopped(true);
                    Despawn();
                }
                return;
            }
            if (_isInLine)
            {
                if ((transform.position - _point.transform.position).magnitude <= LineStopDistance)
                {
                    Agent.isStopped = true;
                    AnimationController.CustomerStopped(true);
                }
                return;
            }
            
            if(_isWaitingForLine)
                return;
            
            if (_isMovingToShelf)
            {
                if ((transform.position - _targetShelf.transform.position).magnitude <= ShelfStopDistance)
                {
                    Agent.isStopped = true;
                    AnimationController.CustomerStopped(true);
                    _isMovingToShelf = false;
                    _isItemAvaialble = CheckIfItemAvailable();
                }
                return;
            }
            
            if(_isItemAvaialble)
                TakeItem();
            else
            {
                WaitForItem();
                return;
            }
        }

        private void SelectBuyableItemShelf()
        {
            if(_targetShelf != null)
                return;
            
            _targetShelf = ShelfListRef.Value[Random.Range(0, ShelfListRef.Value.Count)];
        }
        
        public void MoveToShelf()
        {
            Agent.isStopped = true;
            SelectBuyableItemShelf();
            Agent.SetDestination(_targetShelf.transform.position);
            Agent.isStopped = false;
            AnimationController.CustomerStopped(false);
            _isMovingToShelf = true;
        }

        private bool CheckIfItemAvailable()
        {
            _randomInterval = Random.Range(0, 2f);
            if(_targetShelf != null)
                return _targetShelf.HasItem();
            
            return false;
        }

        private void WaitForItem()
        {
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= _randomInterval)
            {
                _isItemAvaialble = CheckIfItemAvailable();
                _elapsedTime = 0;
            }
        }

        private void TakeItem()
        {
            _isItemAvaialble = false;
            _item = _targetShelf.GetLastItem();
            _item.CustomerTakeItem(this);
            _item.transform.SetParent(ItemPosition);
            _item.transform.DOLocalMove(Vector3.zero, .5f);
            GetInLine();
            _isWaitingForLine = true;
        }

        private void GetInLine()
        {
            Register.Value.TakeCustomerInLine(this);
        }

        public void MoveInLine(RegisterLinePoint point)
        {
            _isInLine = true;
            _point = point;
            Agent.SetDestination(_point.transform.position);
            Agent.isStopped = false;
            AnimationController.CustomerStopped(false);
        }

        public void PayAndLeave()
        {
            MoneyRef.Value += _item.ItemSO.Value;
            _isMovingToDespawn = true;
            _isInLine = false;
            Agent.SetDestination(DespawnPoint.Value.position);
            Agent.isStopped = false;
            AnimationController.CustomerStopped(false);
        }

        public bool IsStopped()
        {
            return Agent.isStopped;
        }

        private void Despawn()
        {
            _item.Despawn();
            CustomerCount.Value--;
            LeanPool.Despawn(this);
        }
    }
}