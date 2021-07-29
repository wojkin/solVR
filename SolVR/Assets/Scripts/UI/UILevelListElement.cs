using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class for UI element in levels' list. Contains name of the level and button to load it.
    /// </summary>
    public class UILevelListElement : MonoBehaviour, IUIListElement
    {
        [Tooltip("Level name text.")]
        [SerializeField]
        private TextMeshProUGUI levelName;

        [Tooltip("Button to load a level.")]
        [SerializeField]
        private Button playButton;

        /// <summary>
        /// Populates the fields of a level list element.
        /// Sets levelName filed with listElementData.environmentName and
        /// adds loading level on button onClick event based on listElementData.scene.
        /// </summary>
        /// <param name="listElementData">ScriptableObject.Environment with data to fill UI list element.</param>
        public void Populate(ScriptableObject listElementData)
        {
            var levelData = (Environment) listElementData;
            levelName.text = levelData.environmentName;
            playButton.onClick.AddListener(() =>
                Addressables.LoadSceneAsync(levelData.scene)
                );
        }
    }
}
