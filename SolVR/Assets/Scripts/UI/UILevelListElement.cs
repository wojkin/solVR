using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UILevelListElement : MonoBehaviour, IUIListElement
    {
        [SerializeField]
        private TextMeshProUGUI levelName;

        [SerializeField]
        private Button playButton;

        public void Populate(ScriptableObject listElementData)
        {
            Environment levelData = (Environment) listElementData;
            levelName.text = levelData.environmentName;
            playButton.onClick.AddListener(() =>
                Addressables.LoadSceneAsync(levelData.scene)
                );
        }
    }
}
