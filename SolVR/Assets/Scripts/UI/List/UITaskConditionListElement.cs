using Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.List
{
    /// <summary>
    /// Class representing an element of a <see cref="UIList"/> displaying task condition's name, description and state.
    /// </summary>'
    public class UITaskConditionListElement : MonoBehaviour, IUIListElement
    {
        #region Serialized Fields

        /// <summary>Text for displaying the task condition state.</summary>
        [SerializeField] private TextMeshProUGUI state;

        /// <summary>Text for displaying the task condition description.</summary>
        [SerializeField] private TextMeshProUGUI description;

        #endregion

        #region Variables

        /// <summary> Task condition that is an element of the list. </summary>
        private ITaskCondition _taskCondition;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets <see cref="state"/> text to default message.
        /// </summary>
        private void SetDefaultState()
        {
            state.text = _taskCondition.GetTaskConditionDescription().GetDefaultStateMessage();
        }

        /// <summary>
        /// Sets <see cref="state"/> text to condition met message.
        /// </summary>
        private void SetConditionMetState()
        {
            state.text = _taskCondition.GetTaskConditionDescription().GetConditionMetStateMessage();
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDestroy()
        {
            _taskCondition?.RemoveListener(SetConditionMetState);
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