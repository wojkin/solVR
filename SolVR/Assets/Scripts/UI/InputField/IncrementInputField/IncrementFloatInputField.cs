using UnityEngine;

namespace UI.InputField.IncrementInputField
{
    /// <summary>
    /// Component for incrementing and decrementing a value of a <see cref="DataTypeInputField"/> with float type.
    /// </summary>
    [RequireComponent(typeof(DataTypeInputField<float>))]
    public class IncrementFloatInputField : IncrementInputField<float>
    {
    }
}