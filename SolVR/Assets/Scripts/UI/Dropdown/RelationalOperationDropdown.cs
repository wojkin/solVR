using System;
using System.Collections.Generic;
using VisualCoding.Values.Enums;

namespace UI.Dropdown
{
    /// <summary>
    /// Class handling dropdown that corresponds to a <c>RelationalOperation</c> enum.
    /// </summary>
    public class RelationalOperationDropdown : EnumDropdown<RelationalOperation>
    {
        #region Custom Methods

        /// <summary>
        /// Populates dropdown with corresponding operators of the enum values.
        /// </summary>
        protected override void PopulateDropdown()
        {
            var enumValues = Enum.GetValues(typeof(RelationalOperation));
            var operators = new List<string>();
            foreach (RelationalOperation enumValue in enumValues) operators.Add(enumValue.GetRelationalOperator());
            dropdown.AddOptions(operators);
        }

        #endregion
    }
}