using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class for displaying level completion menu in front of camera.
    /// </summary>
    [RequireComponent(typeof(UIShowInFrontOfCamera))]
    public class UILevelCompletion : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Text for displaying level completion result.
        /// </summary>
        [SerializeField] private TMP_Text levelResult;

        /// <summary>
        /// Button for reloading level scene.
        /// </summary>
        [SerializeField] private Button tryAgain;

        /// <summary>
        /// Button for loading next level scene.
        /// </summary>
        [SerializeField] private Button nextLevel;

        #endregion

        #region Variables

        /// <summary>Message displayed if level is completed.</summary>
        private const string LevelCompletedMessage = "Level completed!";

        /// <summary>Message displayed if level is failed.</summary>
        private const string LevelFailedMessage = "Level failed!";

        /// <summary>UI with level completion state, that will show in front of camera.</summary>
        private UIShowInFrontOfCamera _uiShowInFrontOfCamera;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            _uiShowInFrontOfCamera = GetComponent<UIShowInFrontOfCamera>();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Deactivates next level button.
        /// </summary>
        public void DeactivateNextLevelButton()
        {
            nextLevel.gameObject.SetActive(false);
        }

        /// <summary>
        /// Add listener to next level selection.
        /// </summary>
        /// <param name="listener">Listener for nextLevel button click.</param>
        public void AddNextLevelListener(UnityAction listener)
        {
            nextLevel.onClick.AddListener(listener);
        }

        /// <summary>
        /// Shows menu ui in front of camera.
        /// </summary>
        /// <param name="levelCompletedSuccessfully">Boolean that represent if level is completed.</param>
        public void ShowResult(bool levelCompletedSuccessfully)
        {
            if (levelCompletedSuccessfully)
            {
                levelResult.text = LevelCompletedMessage;
                nextLevel.interactable = true;
                tryAgain.interactable = false;
            }
            else
            {
                levelResult.text = LevelFailedMessage;
                tryAgain.interactable = true;
                nextLevel.interactable = false;
            }

            _uiShowInFrontOfCamera.Show();
        }

        #endregion
    }
}