using Items;
using UnityEngine;

namespace LevelElements
{
    public class StackPoint : MonoBehaviour
    {
        public bool IsEmpty => Content == null;

        public Item Content;

        public void AddItem(Item item)
        {
            if(IsEmpty)
                Content = item;
        }

        public void RemoveContent()
        {
            if(IsEmpty)
                return;

            Content = null;
        }
    }
}