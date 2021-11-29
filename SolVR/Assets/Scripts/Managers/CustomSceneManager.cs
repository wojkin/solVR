using System;
using System.Collections;
using System.Collections.Generic;
using DeveloperTools;
using Patterns;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Managers
{
    /// <summary>
    /// Class for loading and unloading scenes. Provides events for performing actions before and after scene load.
    /// </summary>
    public class CustomSceneManager : Singleton<CustomSceneManager>
    {
        #region Variables

        /// <summary>Delegate for a scene change (load or unload).</summary>
        public delegate void SceneChange(string sceneName);

        /// <summary>Event invoked after a scene was loaded.</summary>
        public event SceneChange AfterLoad;

        /// <summary>Event invoked before a scene starts unloading.</summary>
        public event SceneChange BeforeUnload;

        /// <summary>Addressable name of the level base scene.</summary>
        private const string LevelBaseAddressableName = "_levelBase";

        /// <summary>Queue of scene load calls.</summary>
        private readonly Queue<Action> _loadQueue = new Queue<Action>();

        private AssetReference _levelBase;

        /// <summary>Flag representing whether a new scene is currently being loaded.</summary>
        private bool _busy;

        /// <summary><see cref="AssetReference"/> of the previously loaded scene.</summary>
        private AssetReference _lastLoadedScene;

        /// <summary><see cref="SceneInstance"/> of the previously loaded scene.</summary>
        private SceneInstance _lastLoadedSceneInstance;

        /// <summary><see cref="LoadSceneMode"/> used to load the previously loaded scene.</summary>
        private LoadSceneMode _lastMode;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Gets <see cref="AssetReference"/> to the level base scene.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            _levelBase = new AssetReference(LevelBaseAddressableName);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Queues a level load or loads it immediately if the manager is not busy.
        /// </summary>
        /// <param name="addressableLevelScene"><see cref="AssetReference"/> to the level scene.</param>
        public void QueueLoadLevel(AssetReference addressableLevelScene)
        {
            QueueLoadScene(_levelBase, LoadSceneMode.Single); // queue loading of the level base scene
            QueueLoadScene(addressableLevelScene, LoadSceneMode.Additive); // queue additive loading of the level scene
        }

        /// <summary>
        /// Queues a scene load or loads it immediately if the manager is not busy.
        /// </summary>
        /// <param name="addressableScene"><see cref="AssetReference"/> to the scene to load.</param>
        /// <param name="mode"><see cref="LoadSceneMode"/> to use when loading the scene.</param>
        public void QueueLoadScene(AssetReference addressableScene, LoadSceneMode mode)
        {
            if (_busy)
                _loadQueue.Enqueue(() => { StartLoadingScene(addressableScene, mode); }); // add load call to queue
            else
                StartLoadingScene(addressableScene, mode);
        }

        /// <summary>
        /// Queues reload of the currently loaded scene or reloads it immediately if the manager is not busy.
        /// </summary>
        public void QueueReloadScene()
        {
            if (_lastLoadedScene != null)
            {
                if (_busy)
                {
                    // if scene was loaded additively add unload call to queue
                    if (_lastMode == LoadSceneMode.Additive)
                        _loadQueue.Enqueue(() => { StartUnloadingScene(_lastLoadedSceneInstance); });

                    // add load call to queue
                    _loadQueue.Enqueue(() => { StartLoadingScene(_lastLoadedScene, _lastMode); });
                }
                else
                {
                    // if scene was loaded additively unload it first
                    if (_lastMode == LoadSceneMode.Additive)
                        StartUnloadingScene(_lastLoadedSceneInstance);

                    StartLoadingScene(_lastLoadedScene, _lastMode); // load scene
                }
            }
            else
            {
                Logger.Log("Cannot reload a scene! No scene was loaded using Addressables yet.");
            }
        }

        /// <summary>
        /// Initializes unloading of a scene.
        /// </summary>
        /// <param name="sceneInstance">Instance fo the scene to unload.</param>
        private void StartUnloadingScene(SceneInstance sceneInstance)
        {
            _busy = true;
            BeforeUnload?.Invoke(SceneManager.GetActiveScene().name);
            StartCoroutine(UnloadSceneAsync(sceneInstance));
        }

        /// <summary>
        /// Initializes loading of a scene.
        /// </summary>
        /// <param name="addressableScene"><see cref="AssetReference"/> to the scene to load.</param>
        /// <param name="mode"><see cref="LoadSceneMode"/> to use when loading the scene.</param>
        private void StartLoadingScene(AssetReference addressableScene, LoadSceneMode mode)
        {
            _busy = true;

            // invoke the before unload event, because loading a scene using the single mode unloads all other scenes
            if (mode == LoadSceneMode.Single)
                BeforeUnload?.Invoke(SceneManager.GetActiveScene().name);

            StartCoroutine(LoadSceneAsync(addressableScene, mode));
        }

        /// <summary>
        /// A coroutine for loading an addressable scene.
        /// </summary>
        /// <param name="addressableScene"><see cref="AssetReference"/> to the scene to load.</param>
        /// <param name="mode"><see cref="LoadSceneMode"/> to use when loading the scene.</param>
        /// <returns><see cref="IEnumerator"/> required for the coroutine.</returns>
        private IEnumerator LoadSceneAsync(AssetReference addressableScene, LoadSceneMode mode)
        {
            // start loading an addressable scene and wait until it finishes loading 
            var asyncLoadLevel = Addressables.LoadSceneAsync(addressableScene, mode);
            while (!asyncLoadLevel.IsDone) yield return null;

            // save data about the last loaded scene
            _lastLoadedScene = addressableScene;
            _lastLoadedSceneInstance = asyncLoadLevel.Result;
            _lastMode = mode;

            AfterLoad?.Invoke(_lastLoadedSceneInstance.Scene.name); // invoke the after-load event

            ProcessQueue();
        }

        /// <summary>
        /// A coroutine for unloading an addressable scene.
        /// </summary>
        /// <param name="sceneInstance">Instance of the scene to unload.</param>
        /// <returns><see cref="IEnumerator"/> required for the coroutine.</returns>
        private IEnumerator UnloadSceneAsync(SceneInstance sceneInstance)
        {
            var asyncUnloadLevel = Addressables.UnloadSceneAsync(sceneInstance);
            while (!asyncUnloadLevel.IsDone) yield return null;

            ProcessQueue();
        }


        /// <summary>
        /// Checks if there are other actions in the queue, if yes start executing the first one.
        /// </summary>
        private void ProcessQueue()
        {
            if (_loadQueue.Count > 0)
                _loadQueue.Dequeue().Invoke();
            else
                _busy = false;
        }

        #endregion
    }
}