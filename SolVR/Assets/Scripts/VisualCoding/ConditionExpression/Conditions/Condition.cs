using UnityEngine;

namespace VisualCoding.ConditionExpression.Conditions
{
    /// <summary>
    /// Class representing condition that can be checked.
    /// </summary>
    public abstract class Condition : MonoBehaviour
    {
        /// <summary>
        /// Checking the condition and a boolean representation of it.
        /// </summary>
        /// <returns>Boolean representation of the condition.
        /// The result is true if the condition is met and otherwise it's false.</returns>
        public abstract bool Check();
    }
}