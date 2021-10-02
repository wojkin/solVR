using System;
using System.Collections;
using System.Collections.Generic;
using DeveloperTools;
using Patterns;
using UnityEngine.AddressableAssets;
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

        /// <summary>Queue of scene load calls.</summary>
        private readonly Queue<Action> _loadQueue = new Queue<Action>();

        /// <summary>Flag representing whether a new scene is currently being loaded.</summary>
        private bool _loading;

        /// <summary>Previously loaded scene.</summary>
        private AssetReference _lastLoadedScene;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Queues a scene load or loads it immediately if no other scene is being loaded.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        public void QueueLoadScene(string sceneName)
        {
            if (_loading)
                _loadQueue.Enqueue(() => { StartLoadingScene(sceneName); }); // add load call to queue
            else
                StartLoadingScene(sceneName); // start loading scene
        }

        /// <summary>
        /// Queues a scene load or loads it immediately if no other scene is being loaded.
        /// </summary>
        /// <param name="addressableScene">AssetReference of the scene to load.</param>
        public void QueueLoadScene(AssetReference addressableScene)
        {
            if (_loading)
                _loadQueue.Enqueue(() => { StartLoadingScene(addressableScene); }); // add load call to queue
            else
                StartLoadingScene(addressableScene);
        }

        /// <summary>
        /// Queues reload of the currently loaded scene or reloads it immediately if no other scene is being loaded.
        /// </summary>
        public void QueueReloadScene()
        {
            if (_lastLoadedScene != null)
            {
                if (_loading)
                    _loadQueue.Enqueue(() => { StartLoadingScene(_lastLoadedScene); }); // add load call to queue
                else
                    StartLoadingScene(_lastLoadedScene);
            }
            else
            {
                Logger.Log("Cannot reload a scene! No scene was loaded using Addressables yet.");
            }
        }

        /// <summary>
        /// Invokes a before-unload event and start a scene load coroutine.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        private void StartLoadingScene(string sceneName)
        {
            _loading = true;
            BeforeUnload?.Invoke(SceneManager.GetActiveScene().name);
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        /// <summary>
        /// Invokes a before-unload event and start an addressable scene load coroutine.
        /// </summary>
        /// <param name="addressableScene">AssetReference of the scene to load.</param>
        private void StartLoadingScene(AssetReference addressableScene)
        {
            _loading = true;
            BeforeUnload?.Invoke(SceneManager.GetActiveScene().name);
            StartCoroutine(LoadSceneAsync(addressableScene));
        }

        /// <summary>
        /// A coroutine for loading a scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // start loading a scene and wait until it finishes loading 
            var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone) yield return null;

            FinishLoadingScene(sceneName);
        }

        /// <summary>
        /// A coroutine for loading an addressable scene.
        /// </summary>
        /// <param name="addressableScene">AssetReference of the scene to load.</param>
        private IEnumerator LoadSceneAsync(AssetReference addressableScene)
        {
            // start loading an addressable scene and wait until it finishes loading 
            var asyncLoadLevel = Addressables.LoadSceneAsync(addressableScene);
            while (!asyncLoadLevel.IsDone) yield return null;

            _lastLoadedScene = addressableScene;
            FinishLoadingScene(asyncLoadLevel.Result.Scene.name);
        }

        /// <summary>
        /// Method for finishing the loading of a scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene which just finished loading.</param>
        private void FinishLoadingScene(string sceneName)
        {
            AfterLoad?.Invoke(sceneName); // invoke the after-load event

            // check if there are other scenes to load, if yes start loading the first one
            if (_loadQueue.Count > 0)
                _loadQueue.Dequeue().Invoke();
            else
                _loading = false;
        }

        #endregion
    }
}