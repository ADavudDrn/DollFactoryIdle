using System.Collections.Generic;
using DefaultNamespace;
using Items;
using Lean.Pool;
using Reference;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelElements
{
    public class Shelf : MonoBehaviour
    {
        public ItemScriptableObject ItemType;

        [SerializeField] private List<StackPoint> StackPoints = new List<StackPoint>();
        [SerializeField] private ShelfListRef ShelfListRef;
        [SerializeField] private bool EditSaveKey;
        [SerializeField,EnableIf(nameof(EditSaveKey))] private string SaveKey;

        private int _initialCount = 0;

        private void OnEnable()
        {
            if(!ShelfListRef.Value.Contains(this))
                ShelfListRef.Value.Add(this);

            Load();
        }

        private void OnDisable()
        {
            if(ShelfListRef.Value.Contains(this))
                ShelfListRef.Value.Remove(this);
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                _initialCount = PlayerPrefs.GetInt(SaveKey, 0);
            }

            for (int i = 0; i < _initialCount; i++)
            {
                var spawnedItem = LeanPool.Spawn(ItemType.Prefab, transform.position, Quaternion.identity);
                spawnedItem.DropItemToShelf(this, true);
            }
        }

        public void GetEmptyStackPoint(out bool doesHaveEmptySpot, out StackPoint firstEmptyPoint)
        {
            doesHaveEmptySpot = false;
            firstEmptyPoint = null;
            
            foreach (var point in StackPoints)
            {
                if (point.IsEmpty)
                {
                    doesHaveEmptySpot = true;
                    firstEmptyPoint = point;
                    break;
                }
            }
        }

        public Item GetLastItem()
        {
            for (int i = StackPoints.Count-1; i >= 0; i--)
            {
                if (!StackPoints[i].IsEmpty)
                    return StackPoints[i].Content;
            }

            return null;
        }

        public bool HasItem()
        {
            foreach (var point in StackPoints)
            {
                if (!point.IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        public void Save()
        {
            int count = 0;
            foreach (var point in StackPoints)
            {
                if (!point.IsEmpty)
                    count++;
            }
            
            PlayerPrefs.SetInt(SaveKey, count);
            PlayerPrefs.Save();
        }
    }
}