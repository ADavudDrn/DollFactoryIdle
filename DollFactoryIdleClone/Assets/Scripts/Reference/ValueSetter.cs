using Sirenix.OdinInspector;
using UnityEngine;

namespace Reference
{
    public class ValueSetter : MonoBehaviour
    {
        private enum SetTime
        {
            Awake,
            OnEnable,
            Start,
        }
        
        [SerializeField, Required] private RefValue Reference;

        [SerializeField] private SetTime SetOn; 

        [SerializeField, ShowIf("@Reference is IntRef"), LabelText("Value"), Indent]
        private int IntValue;
        
        [SerializeField, ShowIf("@Reference is FloatRef"), LabelText("Value"), Indent]
        private float FloatValue;

        [SerializeField, ShowIf("@Reference is BoolRef"), LabelText("Value"), Indent]
        private bool BoolValue;

        [SerializeField, ShowIf("@Reference is TransformRef"), LabelText("Value"), Indent]
        private Transform TransformValue;


        private void Awake()
        {
            if(SetOn != SetTime.Awake)
                return;
            SetValue();
        }

        private void OnEnable()
        {
            if(SetOn != SetTime.OnEnable)
                return;
            SetValue();
        }

        private void Start()
        {
            if(SetOn != SetTime.Start)
                return;
            SetValue();
        }
        
        private void SetValue()
        {
            switch (Reference)
            {
                case IntRef intRef:
                    intRef.Value = IntValue;
                    break;
                case FloatRef floatRef:
                    floatRef.Value = FloatValue;
                    break;
                case BoolRef boolRef:
                    boolRef.Value = BoolValue;
                    break;
                case TransformRef transformRef:
                    transformRef.Value = TransformValue;
                    break;
            }
        }
    }
}