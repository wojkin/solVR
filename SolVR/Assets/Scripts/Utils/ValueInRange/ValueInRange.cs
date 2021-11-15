using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.ValueInRange
{
    /// <summary>
    /// Class representing value of T type in range.
    /// </summary>
    /// <typeparam name="T">Type of value and range</typeparam>
    [Serializable]
    public class ValueInRange<T>
    {
        #region Serialized Fields

        /// <summary>Range that determines the boundaries within which the value must be.</summary>
        [Tooltip("Range that determines the boundaries within which the value must be.")] [SerializeField]
        private Range<T> range;

        /// <summary>Value that must be within range boundaries.</summary>
        [Tooltip("Value that must be within range boundaries.")] [SerializeField]
        private T value;

        /// <summary>An event for initializing range for listeners.</summary>
        [Tooltip("An event for initializing range for listeners.")] [SerializeField]
        private UnityEvent<Range<T>> initializeRange;

        /// <summary>An event that invokes if value changes.</summary>
        [Tooltip("An event that invokes if value changes.")] [SerializeField]
        private UnityEvent<T> onValueChanged;

        #endregion

        #region Variables

        /// <summary>
        /// <inheritdoc cref="value"/>
        /// Clamps value to range and invokes <see cref="onValueChanged"/> event.
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = Range.Clamp(value);
                if (!value.Equals(previous)) onValueChanged.Invoke(this.value);
            }
        }

        /// <summary><inheritdoc cref="range"/></summary>
        public Range<T> Range => range;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invoke initializing events;
        /// </summary>
        public void Initialize()
        {
            initializeRange?.Invoke(Range);
            onValueChanged?.Invoke(value);
        }

        #endregion
    }
}