using System;
using Reference;
using TMPro;
using UnityEngine;

namespace Util
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField] private IntRef MoneyRef;
        [SerializeField] private TextMeshProUGUI Text;

        private const String MONEY_SAVE_KEY = "MONEY_SAVE_KEY";

        private void Awake()
        {
            MoneyRef.Value = 0;
            Load();
            SetText();
        }

        public void SetText()
        {
            Text.SetText(MoneyRef.Value.ToString());
        }

        public void Save()
        {
            PlayerPrefs.SetInt(MONEY_SAVE_KEY, MoneyRef.Value);
            PlayerPrefs.Save();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(MONEY_SAVE_KEY))
            {
                MoneyRef.Value = PlayerPrefs.GetInt(MONEY_SAVE_KEY, 0);
            }
        }
    }
}