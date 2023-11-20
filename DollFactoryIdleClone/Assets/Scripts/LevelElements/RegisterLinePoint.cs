using DefaultNamespace.Customers;
using UnityEngine;

namespace LevelElements
{
    public class RegisterLinePoint : MonoBehaviour
    {
        public bool IsEmpty => Content == null;

        public CustomerController Content;

        public void AddItem(CustomerController customer)
        {
            if(IsEmpty)
                Content = customer;
        }

        public void RemoveContent()
        {
            if(IsEmpty)
                return;

            Content = null;
        }
    }
}