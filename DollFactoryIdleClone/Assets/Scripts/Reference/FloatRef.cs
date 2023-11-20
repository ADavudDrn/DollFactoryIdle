using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "Float Value", menuName = "Values/Float")]
    public class FloatRef : RefValue
    {
        [ShowInInspector]
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }

        private float _value;
    }
}
