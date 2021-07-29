using System.Collections.Generic;
using DeveloperTools.Commands;
using Managers;
using Patterns;
using TMPro;
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

        [Header("Settings")] [Tooltip("Offset between the player and the console.")] [SerializeField]
        private float spawnOffset;

        private readonly List<Command> _commands = new List<Command>(); // list of all available commands
        private Transform _camera; // the camera the console should face
        private bool _isVisible; // a flag showing whether the console is currently visible or hidden 

        /// <summary>
        /// Initializes all commands and hides the console.
        /// </summary>
        public void Awake()
        {
            base.Awake();
            InitializeCommands();
            Hide();
        }

        /// <summary>
        /// Makes the console face the camera when it's visible and manages hiding and showing the console on a button
        /// press.
        /// </summary>
        private void Update()
        {
            // if the console is visible rotate it to face the player
            if (_isVisible)
                FaceCamera();

            // if the primary thumbstick is pressed show or hide the console
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
                if (_isVisible)
                    Hide();
                else
                    Show();
        }

        /// <summary>
        /// Rotates the developer console to always face the player. Interpolates the rotation in order to make it
        /// smoother.
        /// </summary>
        private void FaceCamera()
        {
            // calculate the direction vector from the camera to the console and ignore the vertical difference.
            var lookPos = transform.position - _camera.position;
            lookPos.y = 0;

            // calculate the rotation at which the console would face the camera.
            var rotation = Quaternion.LookRotation(lookPos);

            // Interpolate between the current console rotation and the desired one.
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100f);
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
        /// Makes the developer console not visible. Messages can still be logged and will be visible after the console
        /// is shown.
        /// </summary>
        public void Hide()
        {
            ui.SetActive(false);
            _isVisible = false;
        }

        /// <summary>
        /// Makes the developer console visible.
        /// </summary>
        private void Show()
        {
            _camera = Camera.main.gameObject.transform; // find the main camera object

            // calculate the position for the console in front of the player
            transform.position = _camera.position + _camera.forward * spawnOffset;

            ui.SetActive(true);
            _isVisible = true;
        }

        /// <summary>
        /// This function will be called before a scene is unloaded. The developer console is hidden and the
        /// moved to DDOL scene. This makes the developer console persist between scene loads.
        /// </summary>
        /// <param name="sceneName">Name of the scene which will be unloaded.</param>
        private void BeforeUnloadHandler(string sceneName)
        {
            Hide();
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