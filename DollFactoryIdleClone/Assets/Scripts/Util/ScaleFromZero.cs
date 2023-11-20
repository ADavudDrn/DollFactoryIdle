using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class ScaleFromZero : MonoBehaviour
    {
        public UnityEvent OnGrowComplete;
    
        [SerializeField] private float DurationInSec = 0.25f;

        [SerializeField] private bool RandomDelay = false;

        [HideIf(nameof(RandomDelay)), SerializeField]
        private float Delay = 0f;

        [ShowIf(nameof(RandomDelay)), SerializeField]
        private float MinDelay = 0f;

        [ShowIf(nameof(RandomDelay)), SerializeField]
        private float MaxDelay = 0f;

        [SerializeField] private Ease EaseType = Ease.OutBack;

        [SerializeField] private float Overshoot = 1.70158f;
    
        private bool _isFirstTime = true;

        private void Awake()
        {
            Shrink();
        }

        private void OnEnable()
        {
            var delay = RandomDelay ? Random.Range(MinDelay, MaxDelay) : Delay;
            if (_isFirstTime)
                return;

            Shrink();
            PlayAnimation(delay);
            _isFirstTime = false;
        }

        private void OnDisable()
        {
            Shrink();
        }

        private void PlayAnimation(float delay)
        {
            var tween = transform.DOScale(Vector3.one, DurationInSec).SetDelay(delay).SetEase(EaseType)
                .OnComplete(() => { OnGrowComplete?.Invoke(); });
            if (EaseType == Ease.OutBack)
                tween.easeOvershootOrAmplitude = Overshoot;
        }

        private void Shrink()
        {
            transform.localScale = Vector3.zero;
        }
    }
}