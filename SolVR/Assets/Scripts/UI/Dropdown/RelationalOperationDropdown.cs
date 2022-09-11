using System;
using System.Collections.Generic;
using VisualScripting.Values.Enums;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to a <see cref="RelationalOperator"/> enum.
    /// </summary>
    public class RelationalOperationDropdown : EnumDropdown<RelationalOperator>
    {
        #region Custom Methods

        /// <summary>
        /// Populates dropdown with corresponding operators of the enum values.
        /// </summary>
        protected override void PopulateDropdown()
        {
            var enumValues = Enum.GetValues(typeof(RelationalOperator));
            var operators = new List<string>();
            foreach (RelationalOperator enumValue in enumValues) operators.Add(enumValue.GetRelationalOperator());
            dropdown.AddOptions(operators);
        }

        #endregion
    }
}