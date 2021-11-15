using System;
using System.Collections.Generic;
using VisualScripting.Values.Enums;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to a <see cref="ArithmeticOperation"/> enum.
    /// </summary>
    public class ArithmeticOperationDropdown : EnumDropdown<ArithmeticOperation>
    {
        #region Custom Methods

        /// <summary>
        /// Populates dropdown with corresponding operators of the enum values.
        /// </summary>
        protected override void PopulateDropdown()
        {
            var enumValues = Enum.GetValues(typeof(ArithmeticOperation));
            var operators = new List<string>();
            foreach (ArithmeticOperation enumValue in enumValues) operators.Add(enumValue.GetArithmeticOperator());
            dropdown.AddOptions(operators);
        }

        #endregion
    }
}