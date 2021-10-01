using System.Collections.Generic;
using DeveloperTools.Commands;
using Patterns;
using TMPro;
using UnityEngine;

namespace DeveloperTools
{
    /// <summary>
    /// Class for executing commands and logging messages using the developer console prefab.
    /// </summary>
    public class DeveloperConsole : SingleGlobalInstance<DeveloperConsole>
    {
        #region Serialized Fields

        /// <summary>Content of the console scroll view.</summary>
        [Header("User Interface")] [Tooltip("Content of the console scroll view.")] [SerializeField]
        private Transform content;

        /// <summary>Template for an element of the scroll view.</summary>
        [Tooltip("Template for an element of the scroll view.")] [SerializeField]
        private GameObject listEntryTemplate;

        /// <summary>Input file for the commands.</summary>
        [Tooltip("Input file for the commands.")] [SerializeField]
        private TMP_InputField inputField;

        #endregion

        #region Variables

        /// <summary>List of all available commands.</summary>
        private readonly List<Command> _commands = new List<Command>();

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes all commands.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            InitializeCommands();
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            Logger.LogEvent += Log;
        }

        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            Logger.LogEvent -= Log;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Displays a message in a scrollable list in the developer console.
        /// </summary>
        /// <param name="msg">The string that will be displayed in the developer console.</param>
        private void Log(string msg)
        {
            CreateListEntry(msg);
        }

        /// <summary>
        /// Adds an entry to the scrollable list.
        /// </summary>
        /// <param name="msg">The string which will be displayed as the list entry.</param>
        private void CreateListEntry(string msg)
        {
            var listEntry = Instantiate(listEntryTemplate, content, false);
            var consoleEntry = listEntry.GetComponent<DeveloperConsoleEntry>();
            consoleEntry.SetText(msg);
        }

        /// <summary>
        /// Adds all commands to a list, so they can be accessed through the console.
        /// </summary>
        private void InitializeCommands()
        {
            _commands.Add(new ResetCommand());
        }

        /// <summary>
        /// Tries to execute a string from an input filed as a command.
        /// </summary>
        public void TryExecuteCommand()
        {
            var cmd = inputField.text; // get the command string from the input field
            if (cmd != "")
            {
                // for each available command check if the string matches its pattern
                foreach (var command in _commands)
                    if (command.CheckMatch(cmd))
                    {
                        // if the string matches a pattern, execute the command and log it in the console
                        command.Execute();
                        Log(cmd);

                        // clear the input filed
                        inputField.text = "";
                        inputField.textComponent.color = Color.black;
                        return;
                    }

                // if no pattern matched the input, change the text color to red
                inputField.textComponent.color = Color.red;
            }
        }

        #endregion
    }
}