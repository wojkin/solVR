namespace Utils.ValueInRange.RangeConverter
{
    /// <summary>
    /// Class for converting float value in range into integer value in different range.
    /// </summary>
    public class FloatToIntValueInRangeConverter : ValueInRangeConverter<float, int>
    {
        #region Custom Methods

        /// <summary>
        /// Converts value from int to float. Conversion based on theirs' ranges.
        /// </summary>
        /// <param name="value">An integer value that will be converted to float.</param>
        /// <returns>Converted float value.</returns>
        protected override float ConvertValueOutToIn(int value)
        {
            var outDifference = outValue.Range.upperBound - outValue.Range.lowerBound;
            var inDifference = inRange.upperBound - inRange.lowerBound;
            return (((value - outValue.Range.lowerBound) * inDifference) / outDifference) + inRange.lowerBound;
        }

        /// <summary>
        /// Converts value from float to int. Conversion based on theirs' ranges.
        /// </summary>
        /// <param name="value">An float value that will be converted to integer.</param>
        /// <returns>Converted integer value.</returns>
        protected override int ConvertValueInToOut(float value)
        {
            var outDifference = outValue.Range.upperBound - outValue.Range.lowerBound;
            var inDifference = inRange.upperBound - inRange.lowerBound;
            return (int)(((value - inRange.lowerBound) * outDifference / inDifference) + outValue.Range.lowerBound);
        }

        #endregion
    }
}