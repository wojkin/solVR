using UnityEngine;

namespace VisualCoding.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value.
    /// </summary>
    public abstract class BooleanValue : MonoBehaviour
    {
        /// <summary>
        /// Returns a boolean value.
        /// </summary>
        /// <returns>A value of a boolean type.</returns>
        public abstract bool GetValue();
    }
}