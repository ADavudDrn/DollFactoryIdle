using Cinemachine;
using Reference;
using UnityEngine;

namespace Util
{
    public class CameraDistanceSetter : MonoBehaviour
    { 
        [SerializeField] private FloatRef CameraDistanceRef;
        
        private CinemachineVirtualCamera _cVam;
        private CinemachineFramingTransposer _framingTransposer;
        
        private void Awake()
        {
            _cVam = GetComponent<CinemachineVirtualCamera>();
            _framingTransposer = _cVam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void Start()
        {
            UpdateCameraDistance();
        }

        private void OnEnable()
        {
            CameraDistanceRef.OnValueChanged += UpdateCameraDistance;
        }

        private void OnDisable()
        {
            CameraDistanceRef.OnValueChanged += UpdateCameraDistance;
        }

        private void UpdateCameraDistance()
        {
            _framingTransposer.m_CameraDistance = CameraDistanceRef.Value;
        }
    }
}