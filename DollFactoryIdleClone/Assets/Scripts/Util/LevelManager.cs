using System;
using System.Collections.Generic;
using Reference;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Util
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private IntRef CurrentLevel;
        [SerializeField] private List<GameObject> LevelPrefabs;

        [SerializeField] private bool EditSaveKey;

        [SerializeField, EnableIf(nameof(EditSaveKey))]
        private String SaveKey;

        private void Awake()
        {
            Load();
            InstantiateCurrentLevel();
        }

        private void InstantiateCurrentLevel()
        {
            if (LevelPrefabs.Count <= 0)
                return;

            Instantiate(LevelPrefabs[(CurrentLevel.Value-1) % LevelPrefabs.Count]);

        }

        private void Load()
        {
            CurrentLevel.Value = PlayerPrefs.HasKey(SaveKey)
                ? PlayerPrefs.GetInt(SaveKey, 1)
                : 1;
        }

        private void Save()
        {
            PlayerPrefs.SetInt(SaveKey, CurrentLevel.Value);
            PlayerPrefs.Save();
        }

        public void LevelCompleted()
        {
            CurrentLevel.Value++;
            Save();
        }

        [Button]
        public void DeleteSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
    


