using Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.List
{
    /// <summary>
    /// Class representing an element of a <see cref="UIList"/> displaying task condition's name, description and state.
    /// </summary>'
    public class UITaskConditionListElement : MonoBehaviour, IUIListElement
    {
        #region Serialized Fields

        /// <summary>Image for displaying the task condition state.</summary>
        [SerializeField] private Image stateImage;

        /// <summary>Text for displaying the task condition description.</summary>
        [SerializeField] private TextMeshProUGUI description;

        #endregion

        #region Variables

        /// <summary> Task condition that is an element of the list. </summary>
        private ITaskCondition _taskCondition;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDestroy()
        {
            _taskCondition?.RemoveListener(SetConditionMetState);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets <see cref="stateImage"/> to disabled hide image.
        /// </summary>
        private void SetDefaultState()
        {
            stateImage.enabled = false;
        }

        /// <summary>
        /// Sets <see cref="stateImage"/> to enabled to display image with state information.
        /// </summary>
        private void SetConditionMetState()
        {
            stateImage.enabled = true;
        }

        #endregion

        #region IUIListElement Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="listElementData">
        /// <see cref="ITaskCondition"/> used to populate the list element.
        /// Add listener to change state.
        /// </param>
        public void Populate(Object listElementData)
        {
            _taskCondition = listElementData.GetComponent<ITaskCondition>();
            description.text = _taskCondition.GetTaskConditionDescription().GetDescription();
            SetDefaultState();
            _taskCondition.AddListener(SetConditionMetState);
        }

        #endregion
    }
}