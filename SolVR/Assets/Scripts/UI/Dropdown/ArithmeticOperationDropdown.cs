using System;
using System.Collections.Generic;
using VisualCoding.Values.Enums;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to a <c>ArithmeticOperation</c> enum.
    /// </summary>
    public class ArithmeticOperationDropdown : EnumDropdown<ArithmeticOperation>
    {
        /// <summary>
        /// Populates dropdown with corresponding operators of the enum values.
        /// </summary>
        protected override void PopulateDropdown()
        {
            var enumValues = Enum.GetValues(typeof(ArithmeticOperation));
            var operators = new List<string>();
            foreach (ArithmeticOperation enumValue in enumValues)
            {
                operators.Add(enumValue.GetArithmeticOperator());
            }
            dropdown.AddOptions(operators);
        }
    }
}
