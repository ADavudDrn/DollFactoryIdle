using Sirenix.OdinInspector;
using UnityEngine;

namespace Util
{
    public class DeleteSave : MonoBehaviour
    {
        [Button]
        public void DeleteSaveData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}