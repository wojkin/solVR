namespace UI.Slider
{
    /// <summary>
    /// Class for initialize slider values to values in float range.
    /// </summary>
    public class SliderInFloatRange : SliderInRange<float>
    {
        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="SliderInRange{T}.onSliderValueChanged"/> event with float.
        /// </summary>
        /// <param name="value">Value that will be passed.</param>
        protected override void InvokeSliderParsedValueOnChange(float value)
        {
            onSliderValueChanged.Invoke(value);
        }

        #endregion
    }
}