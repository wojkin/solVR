using TMPro;
using UnityEngine;

namespace DeveloperTools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DeveloperConsoleEntry : MonoBehaviour
    {
        private TextMeshProUGUI _entryText;

        public void SetText(string text)
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

            if (_entryText == null)
                _entryText = GetComponent<TextMeshProUGUI>();

            _entryText.text = text;
        }
    }
}