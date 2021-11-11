using UnityEngine;

namespace UI.InputField.IncrementInputField
{
    /// <summary>
    /// Component for incrementing and decrementing a value of a <see cref="DataTypeInputField"/> with int type.
    /// </summary>
    [RequireComponent(typeof(DataTypeInputField<int>))]
    public class IncrementIntegerInputField : IncrementInputField<int>
    {
    }
}