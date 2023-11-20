using UnityEngine;

namespace Util
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = _mainCamera.transform.rotation * Quaternion.Euler(0f, -1f, 0f);
        }
    }
}