using System;
using System.Collections.Generic;
using DeveloperTools.Commands;
using Managers;
using Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeveloperTools
{
    public class DeveloperConsole : SingleGlobalInstance<DeveloperConsole>
    {
        [SerializeField] private GameObject ui;
        [SerializeField] private Transform content;
        [SerializeField] private GameObject listEntryTemplate;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Canvas worldSpaceCanvas;
        private readonly List<Command> _commands = new List<Command>();
        private Transform _camera;
        private bool _isVisible;

        public void Awake()
        {
            base.Awake();
            InitializeCommands();
            Hide();
        }

        private void Update()
        {
            if (_isVisible)
                FaceCamera();
        }

        private void FaceCamera()
        {
            var lookPos = transform.position - _camera.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100f);
        }

        private void OnEnable()
        {
            CustomSceneManager.Instance.AfterLoad += AfterLoadHandler;
            CustomSceneManager.Instance.BeforeUnload += BeforeUnloadHandler;
            Logger.LogEvent += Log;
        }

        private void OnDisable()
        {
            if (CustomSceneManager.Instance != null)
            {
                CustomSceneManager.Instance.AfterLoad -= AfterLoadHandler;
                CustomSceneManager.Instance.BeforeUnload -= BeforeUnloadHandler;
            }

            Logger.LogEvent -= Log;
        }

        private void Log(string msg)
        {
            CreateListEntry(msg);
            Debug.Log(msg);
        }

        private void CreateListEntry(string msg)
        {
            var listEntry = Instantiate(listEntryTemplate, content, false);
            var consoleEntry = listEntry.GetComponent<DeveloperConsoleEntry>();
            consoleEntry.SetText(msg);
        }

        public void Hide()
        {
            ui.SetActive(false);
            _isVisible = false;
        }

        public void Show()
        {
            _camera = Camera.main.gameObject.transform;
            ui.SetActive(true);
            _isVisible = true;
        }

        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }

        private void BeforeUnloadHandler(string sceneName)
        {
            Hide();
            DontDestroyOnLoad(gameObject);
        }

        private void AfterLoadHandler(string sceneName)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
            worldSpaceCanvas.worldCamera = Camera.main;
        }

        private void InitializeCommands()
        {
            _commands.Add(new ResetCommand());
        }

        public void TryExecuteCommand()
        {
            var cmd = inputField.text;
            if (cmd != "")
            {
                foreach (var command in _commands)
                    if (command.CheckMatch(cmd))
                    {
                        command.Execute();
                        Log(cmd);
                        inputField.text = "";
                        inputField.textComponent.color = Color.black;
                        return;
                    }

                inputField.textComponent.color = Color.red;
            }
        }
    }
}