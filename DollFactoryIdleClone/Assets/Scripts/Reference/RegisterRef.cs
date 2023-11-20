using DefaultNamespace;
using LevelElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    [CreateAssetMenu(fileName = "Register Value", menuName = "Values/Register")]
    public class RegisterRef : RefValue
    {
        [ShowInInspector]
        public Register Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        
        private Register _value;
    }
}