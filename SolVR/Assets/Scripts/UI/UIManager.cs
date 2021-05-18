using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private UIElement displayed;
        
        private void Start()
        {
            displayed?.Show();
        }

        

        public void ShowElement(UIElement element)
        {
            displayed?.Hide();
            element.Show();
            displayed = element;
        }

    }
}
