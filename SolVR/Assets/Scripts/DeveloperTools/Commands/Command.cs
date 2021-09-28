using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DeveloperTools.Commands
{
    /// <summary>
    /// A base class for all commands, which can be executed thorough the developer console.
    /// </summary>
    [Serializable]
    public class Command
    {
        #region Serialized Fields

        /// <summary>
        /// A regex pattern representing the command.
        /// If a string is matched to this pattern the command will be executed.
        /// </summary>
        [SerializeField] protected string commandPattern;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Executes the command.
        /// </summary>
        public virtual void Execute()
        {
        }

        /// <summary>
        /// Checks if a string matches the pattern of this command.
        /// </summary>
        /// <param name="command">String which match will be checked.</param>
        /// <returns>A bool representing whether the string matches with the pattern.</returns>
        public bool CheckMatch(string command)
        {
            var rx = new Regex(commandPattern);
            return rx.IsMatch(command);
        }

        #endregion
    }
}