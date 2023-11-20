using Reference;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float CurrentMoveSpeed;
        [SerializeField] private FloatRef NormalizedMoveSpeed;
        [SerializeField] private string JoystickName;

        private float MaxSpeed => 5;
    
        private float _joystickDistance =>
            new Vector2(_joystick.GetHorizontalAxis(), _joystick.GetVerticalAxis()).magnitude;
        private UltimateJoystick _joystick;
        private Rigidbody _rigidbody;
        private bool _isActive;
    
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _joystick = UltimateJoystick.GetUltimateJoystick(JoystickName);
            SetMovementActive(true);
        }

        private void Update()
        {
            if (!_isActive)
                return;
            CurrentMoveSpeed = _joystickDistance * MaxSpeed;
            NormalizedMoveSpeed.Value = CurrentMoveSpeed / MaxSpeed;
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;
            HandlePosition();
            HandleRotation();
        }
    
    
        [Button]
        public void SetMovementActive(bool flag)
        {
            _isActive = flag;
        }
    
    
        private void HandlePosition()
        {
            var forwardSpeed = transform.forward * CurrentMoveSpeed/*.Value*/;
            var targetVelocity = new Vector3(forwardSpeed.x, _rigidbody.velocity.y, forwardSpeed.z);

            _rigidbody.velocity = targetVelocity;
        }

        private void HandleRotation()
        {
            var currentJoystickRotation = new Vector3(_joystick.GetHorizontalAxis(), 0f, _joystick.GetVerticalAxis());
            if(currentJoystickRotation == Vector3.zero)
                return;
            var lookTarget = Quaternion.LookRotation(currentJoystickRotation);
            var rot = Quaternion.Lerp(_rigidbody.rotation, lookTarget, .6f);
            _rigidbody.MoveRotation(rot);
        }
    }
}
