using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unlockable
{
    [CreateAssetMenu(fileName = "UnlockableSO", menuName = "UnlockableSO", order = 0)]
    public class UnlockableSO : ScriptableObject
    {
        public int Cost;
        public bool IsUnlocked;

        [SerializeField] private bool EditSaveKey;

        [SerializeField, EnableIf(nameof(EditSaveKey))]
        private String SaveKey;

        public void Save()
        {
            var value = IsUnlocked ? 1 : 0;
            PlayerPrefs.SetInt(SaveKey, value);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            var value = PlayerPrefs.GetInt(SaveKey, 0);

            IsUnlocked = value == 1;
        }

        public void Unlock()
        {
            IsUnlocked = true;
            Save();
        }
    }
}