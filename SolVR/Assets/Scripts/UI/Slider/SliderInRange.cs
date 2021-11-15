using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace UI.Slider
{
    /// <summary>
    /// Class for initialize slider values to values in range.
    /// </summary>
    /// <typeparam name="T">Type of range values.</typeparam>
    [RequireComponent(typeof(UnityEngine.UI.Slider))]
    public abstract class SliderInRange<T> : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>An event that invokes on <see cref="slider"/> value changed.</summary>
        [Tooltip("An event that invokes on slider value changed.")] [SerializeField]
        public UnityEvent<T> onSliderValueChanged;

        #endregion

        #region Variables

        /// <summary>A slider that will be initialize with range values.</summary>
        protected UnityEngine.UI.Slider slider;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes fields.
        /// </summary>
        protected virtual void Awake()
        {
            slider = GetComponent<UnityEngine.UI.Slider>();
        }

        /// <summary>
        /// Subscribes to event. 
        /// </summary>
        protected void OnEnable()
        {
            slider.onValueChanged.AddListener(InvokeSliderParsedValueOnChange);
        }

        /// <summary>
        /// Unsubscribes from event. 
        /// </summary>
        protected void OnDisable()
        {
            slider.onValueChanged.RemoveListener(InvokeSliderParsedValueOnChange);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="onSliderValueChanged"/> event with parsed value of T type from float.
        /// </summary>
        /// <param name="value">Value that will be parsed.</param>
        protected abstract void InvokeSliderParsedValueOnChange(float value);

        /// <summary>
        /// Initializes slider minimum and maximum values.
        /// </summary>
        /// <param name="range">A range that will be used to initialized slider values.</param>
        public void InitializeSliderValues(Range<T> range)
        {
            slider.minValue = (dynamic)range.lowerBound;
            slider.maxValue = (dynamic)range.upperBound;
            slider.value = (dynamic)range.lowerBound;
        }

        /// <summary>
        /// Sets slider value.
        /// </summary>
        /// <param name="value">A value that will be set to slider value.</param>
        public void SetSliderValue(T value)
        {
            slider.value = (dynamic)value;
        }

        #endregion
    }
}