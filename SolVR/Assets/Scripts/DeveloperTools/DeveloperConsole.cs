using System.Collections.Generic;
using DeveloperTools.Commands;
using Managers;
using Patterns;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeveloperTools
{
    /// <summary>
    /// Class for executing commands and logging messages using the developer console prefab.
    /// </summary>
    public class DeveloperConsole : SingleGlobalInstance<DeveloperConsole>
    {
        [Header("User Interface")] [Tooltip("UI of the developer console.")] [SerializeField]
        private GameObject ui;

        [Tooltip("Content of the console scroll view.")] [SerializeField]
        private Transform content;

        [Tooltip("Template for an element of the scroll view.")] [SerializeField]
        private GameObject listEntryTemplate;

        [Tooltip("Input file for the commands.")] [SerializeField]
        private TMP_InputField inputField;

        [Tooltip("Canvas of the developer console UI.")] [SerializeField]
        private Canvas worldSpaceCanvas;
        
        private readonly List<Command> _commands = new List<Command>(); // list of all available commands
        
        [Tooltip("UIElement attached to the developer console UI canvas.")] [SerializeField]
        private UIElement uiElement;

        /// <summary>
        /// Initializes all commands.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            InitializeCommands();
        }
        
        /// <summary>
        /// Hides the console.
        /// </summary>
        public void Start()
        {
            uiElement.Hide();
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            // subscribe to CustomSceneManager events for loading scenes
            CustomSceneManager.Instance.AfterLoad += AfterLoadHandler;
            CustomSceneManager.Instance.BeforeUnload += BeforeUnloadHandler;

            // subscribe to an message log event
            Logger.LogEvent += Log;
        }

        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            // unsubscribe from previously subscribed events
            if (CustomSceneManager.Instance != null)
            {
                CustomSceneManager.Instance.AfterLoad -= AfterLoadHandler;
                CustomSceneManager.Instance.BeforeUnload -= BeforeUnloadHandler;
            }

            Logger.LogEvent -= Log;
        }

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
        /// This function will be called before a scene is unloaded. The developer console is hidden and the
        /// moved to DDOL scene. This makes the developer console persist between scene loads.
        /// </summary>
        /// <param name="sceneName">Name of the scene which will be unloaded.</param>
        private void BeforeUnloadHandler(string sceneName)
        {
            uiElement.Hide();
            DontDestroyOnLoad(gameObject); // move the console to DDOL
        }

        /// <summary>
        /// This function will be called after a scene is loaded. The developer console is moved from
        /// DDOL to the currently loaded scene, so it can be accessed by other scripts.
        /// </summary>
        /// <param name="sceneName">Name of the scene, which was loaded.</param>
        private void AfterLoadHandler(string sceneName)
        {
            // move the console to the current scene
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
            worldSpaceCanvas.worldCamera = Camera.main; // set the UI camera to the main camera in the loaded scene
        }

        /// <summary>
        /// Adds all commands to a list, so they can be accessed through the console.
        /// </summary>
        private void InitializeCommands()
        {
            _commands.Add(new ResetCommand());
        }

        /// <summary>
        /// Tries to execute a string from an input filed as a commend. If the string matches a pattern of any command
        /// it's executed, otherwise the text color changes to red and no command is executed.
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
    }
}