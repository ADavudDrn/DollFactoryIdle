using System.Collections.Generic;
using System.Linq;
using Items;
using Lean.Pool;
using UnityEngine;

namespace LevelElements
{
    public class Production : MonoBehaviour
    {
        [SerializeField] private Item ItemPrefab;
        [SerializeField] private float ItemSpawnInterval;
        [SerializeField] private int MaxStackCount;
        [SerializeField] private float StackIncrement;
        
        private List<Item> _spawnedItems = new List<Item>();
        private float _elapsedTime;

        private void Update()
        {
            if(_spawnedItems.Count >= MaxStackCount)
                return;
            
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= ItemSpawnInterval)
            {
                var spawnedItem = LeanPool.Spawn(ItemPrefab, GetStackPosition(), Quaternion.identity);
                _spawnedItems.Add(spawnedItem);
                spawnedItem.Producer = this;
                _elapsedTime = 0;
            }
        }

        private Vector3 GetStackPosition()
        {
            var pos = new Vector3(transform.position.x, transform.position.y + _spawnedItems.Count * StackIncrement,
                transform.position.z);
            return pos;
        }
            
            
        public Item GetLastItem()
        {
            return _spawnedItems.Last();
        }

        public void RemoveItem(Item item)
        {
            if (_spawnedItems.Contains(item))
                _spawnedItems.Remove(item);

            item.Producer = null;
        }
        
    }
}