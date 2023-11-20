using System;
using Reference;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private FloatRef NormalizedSpeed;

        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UpdateSpeed()
        {
            if (NormalizedSpeed.Value < 0.1f)
                _animator.SetFloat(Speed, 0);

            _animator.SetFloat(Speed, NormalizedSpeed.Value);
        }
    }
}