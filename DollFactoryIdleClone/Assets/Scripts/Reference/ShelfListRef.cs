using System.Collections.Generic;
using DefaultNamespace;
using LevelElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "ShelfList Value", menuName = "Values/ShelfList")]
    public class ShelfListRef : RefValue
    {
        [ShowInInspector]
        public List<Shelf> Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        
        private List<Shelf> _value = new List<Shelf>();
    }
}