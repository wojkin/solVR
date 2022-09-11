using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Struct for storing range lower and upper bound and clamping value to match range. 
    /// </summary>
    /// <typeparam name="T">Type of the value checked by range.</typeparam>
    [Serializable]
    public struct Range<T>
    {
        #region Serialized Fields

        /// <summary>The lower bound of the range.</summary>
        [Tooltip("The lower bound of the range.")] [SerializeField]
        public T lowerBound;

        /// <summary>The upper bound of the range.</summary>
        [Tooltip("The upper bound of the range.")] [SerializeField]
        public T upperBound;

        /// <summary>Flag showing if range ignores a lower bound.</summary>
        [Tooltip("Flag showing if range ignores a lower bound.")] [SerializeField]
        private bool ignoreLowerBound;

        /// <summary>Flag showing if range ignores an upper bound.</summary>
        [Tooltip("Flag showing if range ignores an upper bound.")] [SerializeField]
        private bool ignoreUpperBound;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Checks value and clamps it so that it is within the range.
        /// </summary>
        /// <param name="valueToClamp">Value to clamp to range.</param>
        /// <returns>Clamped value checked by range bounds.</returns>
        public T Clamp(T valueToClamp)
        {
            var clumpedByLowerBound =
                !ignoreLowerBound && (dynamic)valueToClamp < lowerBound ? lowerBound : valueToClamp;
            return !ignoreUpperBound && (dynamic)clumpedByLowerBound > upperBound ? upperBound : clumpedByLowerBound;
        }

        #endregion
    }
}