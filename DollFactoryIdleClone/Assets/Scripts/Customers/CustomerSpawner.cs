using System;
using Lean.Pool;
using Reference;
using UnityEngine;

namespace DefaultNamespace.Customers
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private CustomerController Prefab;
        [SerializeField] private float SpawnInterval;
        [SerializeField] private int MaxSpawnCount;
        [SerializeField] private IntRef SpawnedCustomerCount;

        private float _elapsedTime;

        private void Awake()
        {
            SpawnedCustomerCount.Value = 0;
        }

        private void Update()
        {
            if(SpawnedCustomerCount.Value >= MaxSpawnCount)
                return;

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= SpawnInterval)
            {
                SpawnCustomer();
                _elapsedTime = 0;
            }
        }

        private void SpawnCustomer()
        {
            var customer = LeanPool.Spawn(Prefab, transform.position, Quaternion.identity);
            customer.MoveToShelf();
            SpawnedCustomerCount.Value++;
        }
    }
}