using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "Transform Reference", menuName = "Values/Transform")]
    public class TransformRef : RefValue
    {
        [ShowInInspector]
        public Transform Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private Transform _value;
    }
}