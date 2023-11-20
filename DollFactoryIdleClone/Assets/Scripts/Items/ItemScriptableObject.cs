using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public int Value;
        public Item Prefab;
    }
}