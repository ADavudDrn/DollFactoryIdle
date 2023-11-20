using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Customers;
using Player;
using Reference;
using UnityEngine;

namespace LevelElements
{
    public class Register : MonoBehaviour
    {
        [SerializeField] private RegisterRef RegisterRef;
        [SerializeField] private List<RegisterLinePoint> LinePoints = new List<RegisterLinePoint>();

        [SerializeField] private float LinePointDistance = 1f;
        
        private float _elapsedTime;
        private bool _isPlayerAtRegister;
        
        
        private void Awake()
        {
            RegisterRef.Value = this;
            for (int i = 0; i < LinePoints.Count; i++)
            {
                LinePoints[i].transform.localPosition = new Vector3(0, 0, (i+1) * LinePointDistance);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent(out PlayerStacker player))
                return;

            _isPlayerAtRegister = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.TryGetComponent(out PlayerStacker player))
                return;

            _isPlayerAtRegister = false;
            _elapsedTime = 0;
        }

        private void Update()
        {
            if(!_isPlayerAtRegister)
                return;
            
            if(LinePoints.First().IsEmpty)
                return;
            
            if(!LinePoints.First().Content.IsStopped())
                return;
            
            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime >= 1f)
            {
                MoveCustomersInLine();
                _elapsedTime = 0;
            }
        }

        public void TakeCustomerInLine(CustomerController customer)
        {
            var point = FindFirstEmptyPoint();
            if (point != null)
            {
                customer.MoveInLine(point);
                point.AddItem(customer);
            }
        }

        public void MoveCustomersInLine()
        {
            for (int i = 0; i < LinePoints.Count; i++)
            {
                var point = LinePoints[i];
                if (i == 0)
                {
                    if(point.IsEmpty)
                        continue;
                    
                    point.Content.PayAndLeave();
                    point.RemoveContent();
                }
                else
                {
                    var prevPoint = LinePoints[i - 1];
                    var customer = point.Content;
                    if(customer)
                    {
                        customer.MoveInLine(prevPoint);
                        prevPoint.AddItem(customer);
                    }
                    point.RemoveContent();
                }
            }
        }

        private RegisterLinePoint FindFirstEmptyPoint()
        {
            foreach (var point in LinePoints)
            {
                if (point.IsEmpty)
                    return point;
            }

            return null;
        }
    }
}