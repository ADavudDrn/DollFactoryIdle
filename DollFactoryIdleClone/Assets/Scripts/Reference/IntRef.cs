using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "Int Value", menuName = "Values/Int")]
    public class IntRef : RefValue
    {
        [ShowInInspector]
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        
        private int _value;
    }
}