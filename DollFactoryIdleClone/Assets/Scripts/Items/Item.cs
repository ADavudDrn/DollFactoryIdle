using System;
using DefaultNamespace;
using DefaultNamespace.Customers;
using DG.Tweening;
using Lean.Pool;
using LevelElements;
using Player;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        public ItemScriptableObject ItemSO;
        public Production Producer;

        [SerializeField] private bool IsAvailableForMovement;
        [SerializeField] private StackPoint Point;

        private Shelf _shelf;


        private void OnEnable()
        {
            IsAvailableForMovement = true;
            Point = null;
            _shelf = null;
            Producer = null;
        }

        public void TakeItemFromProduction(PlayerStacker stacker)
        {
            if(!IsAvailableForMovement)
                return;
            
            if(!stacker.HasEmptyStackPoint())
                return;

            IsAvailableForMovement = false;
            Producer.RemoveItem(this);
            var point = stacker.GetStackPoint();
            point.AddItem(this);
            Point = point;
            transform.parent = point.transform;
            stacker.AddToStack(this);
            transform.DOLocalJump(Vector3.zero, 2f,1,.25f).OnComplete(() =>
            {
                transform.localEulerAngles = Vector3.zero;
                IsAvailableForMovement = true;
            });
        }

        public bool DropItemToShelf(Shelf shelf, bool skip = false)
        {
            if(!IsAvailableForMovement)
                return false;

            _shelf = shelf;
            _shelf.GetEmptyStackPoint(out var hasEmptyPoint, out var point);
            
            if(hasEmptyPoint == false)
                return false;

            if(Point != null)
                Point.RemoveContent();
            transform.parent = point.transform;
            Point = point;
            point.AddItem(this);
            IsAvailableForMovement = false;
            if (skip)
            {
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                IsAvailableForMovement = true;
                _shelf.Save();
                return true;
            }
            
            transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
            {
                transform.localEulerAngles = Vector3.zero;
                IsAvailableForMovement = true;
            });
            _shelf.Save();
            return true;
            
            
        }
        
        public void CustomerTakeItem(CustomerController customer)
        {
            if (Point != null)
                Point.RemoveContent();

            Point = null;
            if(_shelf != null)
                _shelf.Save();
        }

        public void Despawn()
        {
            transform.parent = null;
            LeanPool.Despawn(this);
        }
    }
}