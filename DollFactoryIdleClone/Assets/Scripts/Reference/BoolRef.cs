using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "Bool Value", menuName = "Values/Bool", order = 0)]
    public class BoolRef : RefValue
    {
        [ShowInInspector]
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private bool _value;
    }
}