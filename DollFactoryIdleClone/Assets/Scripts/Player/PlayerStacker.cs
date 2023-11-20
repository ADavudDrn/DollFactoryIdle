using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Items;
using Lean.Pool;
using LevelElements;
using UnityEngine;

namespace Player
{
    public class PlayerStacker : MonoBehaviour
    {
        [SerializeField] private Transform PlayerStackTransform;
        [SerializeField] private int MaxStackCount;
        [SerializeField] private float StackHeight;
        [SerializeField] private float ProcessTime;
        [SerializeField] private StackPoint StackPointPrefab;
        [SerializeField] private List<StackPoint> StackPoints = new List<StackPoint>();
        private List<Item> _items = new List<Item>();
        private float _elapsedTime;
        private Production _producer;
        private Shelf _shelf;

        private void Awake()
        {
            for (int i = 0; i < MaxStackCount+1; i++)
            {
                var pos = new Vector3(PlayerStackTransform.position.x,
                    PlayerStackTransform.position.y + (StackHeight * i), PlayerStackTransform.position.z);
                var point = LeanPool.Spawn(StackPointPrefab, pos, Quaternion.identity,
                    PlayerStackTransform);
                StackPoints.Add(point);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Production production))
            {
                _producer = production;
                return;
            }

            if (other.TryGetComponent(out Shelf shelf))
            {
                _shelf = shelf;
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _elapsedTime = 0f;
            if (_producer != null && other.TryGetComponent(out Production production))
            {
                _producer = null;
                return;
            }

            if (_shelf != null && other.TryGetComponent(out Shelf shelf))
            {
                _shelf = null;
                return;
            }
        }

        private void Update()
        {
            if (_producer != null)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= ProcessTime)
                {
                    TryGetItem(_producer.GetLastItem());
                    _elapsedTime = 0f;
                }
                return;
            }

            if (_shelf != null)
            {
                if(_items.Count <= 0)
                    return;
                
                var item = _items.Last();
                if(_shelf.ItemType != item.ItemSO)
                    return;
                
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= ProcessTime)
                {
                    var succesful = item.DropItemToShelf(_shelf);
                    if(succesful)
                        RemoveFromStack(item);
                    _elapsedTime = 0f;
                }
            }
        }

        private void TryGetItem(Item item)
        {
            if(_items.Count >= MaxStackCount)
                return;
            
            item.TakeItemFromProduction(this);
        }

        public void AddToStack(Item item)
        {
            if(!_items.Contains(item))
                _items.Add(item);
        }
        
        public void RemoveFromStack(Item item)
        {
            if(_items.Contains(item))
                _items.Remove(item);
        }
        public StackPoint GetStackPoint()
        {
            foreach (var point in StackPoints)
            {
                if (point.IsEmpty)
                    return point;
            }
            return null;
        }
        
        public bool HasEmptyStackPoint()
        {
            foreach (var point in StackPoints)
            {
                if (point.IsEmpty)
                    return true;
            }
            return false;
        }
    }
}