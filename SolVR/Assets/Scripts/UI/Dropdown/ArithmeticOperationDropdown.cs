using System;
using System.Collections.Generic;
using VisualScripting.Values.Enums;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to a <see cref="ArithmeticOperator"/> enum.
    /// </summary>
    public class ArithmeticOperationDropdown : EnumDropdown<ArithmeticOperator>
    {
        #region Custom Methods

        /// <summary>
        /// Populates dropdown with corresponding operators of the enum values.
        /// </summary>
        protected override void PopulateDropdown()
        {
            var enumValues = Enum.GetValues(typeof(ArithmeticOperator));
            var operators = new List<string>();
            foreach (ArithmeticOperator enumValue in enumValues) operators.Add(enumValue.GetArithmeticOperator());
            dropdown.AddOptions(operators);
        }

        #endregion
    }
}