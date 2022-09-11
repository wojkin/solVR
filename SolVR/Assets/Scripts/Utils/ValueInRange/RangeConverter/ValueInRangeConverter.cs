using UnityEngine;
using UnityEngine.Events;

namespace Utils.ValueInRange.RangeConverter
{
    /// <summary>
    /// Class for converting value in range into value in different range.
    /// </summary>
    /// <typeparam name="TIn">Type of value to conversion.</typeparam>
    /// <typeparam name="TOut">Type of converted value.</typeparam>
    public abstract class ValueInRangeConverter<TIn, TOut> : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Out value from the conversion.</summary>
        [Tooltip("Out value from the conversion.")] [SerializeField]
        protected ValueInRange<TOut> outValue;

        /// <summary>An event that invokes if <see cref="_inValue"/> changes.</summary>
        [Tooltip("An event that invokes if inValue changes.")] [SerializeField]
        private UnityEvent<TIn> onInValueChanged;

        #endregion

        #region Variables

        /// <summary>In range to conversion.</summary>
        protected Range<TIn> inRange;

        /// <summary>In value to conversion.</summary>
        private TIn _inValue;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            outValue.Initialize();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initialize range.
        /// </summary>
        /// <param name="range">A range value to initialize <see cref="inRange"/></param>
        public void InitializeRange(Range<TIn> range)
        {
            inRange = range;
        }

        /// <summary>
        /// Sets <see cref="_inValue"/>, converts it and sets <see cref="outValue"/>.
        /// </summary>
        /// <param name="value">A value in TOut type that will be set to <see cref="outValue"/>.</param>
        public void SetInValue(TIn value)
        {
            if (!_inValue.Equals(value))
            {
                _inValue = value;
                outValue.Value = ConvertValueInToOut(_inValue);
            }
        }


        /// <summary>
        /// Sets <see cref="outValue"/>, converts it and sets <see cref="_inValue"/>.
        /// Invokes <see cref="onInValueChanged"/> if <see cref="_inValue"/> changes.
        /// </summary>
        /// <param name="value">A value in TOut type that will be set to <see cref="outValue"/>.</param>
        public void SetOutValue(TOut value)
        {
            outValue.Value = value;
            var convertedToIn = ConvertValueOutToIn(outValue.Value);
            if (!_inValue.Equals(convertedToIn))
            {
                _inValue = convertedToIn;
                onInValueChanged.Invoke(_inValue);
            }
        }

        /// <summary>
        /// Converts value from TOut type to value in TIn type. Conversion based on theirs' ranges.
        /// </summary>
        /// <param name="value">A value in TOut type that will be converted to TIn type.</param>
        /// <returns>Converted value in TIn.</returns>
        protected abstract TIn ConvertValueOutToIn(TOut value);

        /// <summary>
        /// Converts value from TIn type to value in TOut type. Conversion based on theirs' ranges.
        /// </summary>
        /// <param name="value">A value in TIn type that will be converted to TOut type.</param>
        /// <returns>Converted value in TOut.</returns>
        protected abstract TOut ConvertValueInToOut(TIn value);

        #endregion
    }
}