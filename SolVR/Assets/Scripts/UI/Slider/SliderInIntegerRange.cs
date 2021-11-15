namespace UI.Slider
{
    /// <summary>
    /// Class for initialize slider values to values in integer range.
    /// </summary>
    public class SliderInIntegerRange : SliderInRange<int>
    {
        #region Built-in Methods

        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();
            slider.wholeNumbers = true;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="onSliderValueChanged"/> event with parsed integer value from float.
        /// </summary>
        /// <param name="value">Value that will be parsed.</param>
        protected override void InvokeSliderParsedValueOnChange(float value)
        {
            onSliderValueChanged.Invoke((int)value);
        }

        #endregion
    }
}