using UnityEngine;

namespace DefaultNamespace.Customers
{
    public class CustomerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsStopped = Animator.StringToHash("IsStopped");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void CustomerStopped(bool flag)
        {
            _animator.SetBool(IsStopped, flag);
        }
    }
}