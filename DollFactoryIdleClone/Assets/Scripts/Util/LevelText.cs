using Reference;
using TMPro;
using UnityEngine;

namespace Util
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private IntRef CurrentLevel;
        [SerializeField] private TextMeshProUGUI Text;

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            Text.SetText("Level " + CurrentLevel.Value);
        }
    }
}