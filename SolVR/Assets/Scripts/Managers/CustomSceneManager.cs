using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Managers
{
    /// <summary>
    /// Class for loading and unloading scenes.
    /// Keeps a queue of scenes to load, so that when a scene load is requested before the previous one has finished,
    /// they don't collide. Provides events for performing actions before and after scene load.
    /// </summary>
    public class CustomSceneManager : Singleton<CustomSceneManager>
    {
        #region Variables

        public delegate void SceneChange(string sceneName); // a delegate for a scene change (load or unload)

        public event SceneChange AfterLoad; // an event invoked after a scene was loaded
        public event SceneChange BeforeUnload; // an event invoked before a scene starts unloading
        private readonly Queue<Action> _loadQueue = new Queue<Action>(); // a queue of scene load calls
        private bool _loading; // a flag representing whether a new scene is currently being loaded
        private AssetReference _lastLoadedScene;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Queues a scene load or loads it immediately if no other scene is being loaded.
        /// Checks if another scene is currently being loaded. If yes, a scene load call is added to a queue,
        /// otherwise the scene starts loading.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        public void QueueLoadScene(string sceneName)
        {
            if (_loading)
                _loadQueue.Enqueue(() => { StartLoadingScene(sceneName); });
            else
                StartLoadingScene(sceneName);
        }

        /// <summary>
        /// Queues a scene load or loads it immediately if no other scene is being loaded.
        /// Checks if another scene is currently being loaded. If yes, an addressable scene load call is added to a
        /// queue, otherwise the scene starts loading.
        /// </summary>
        /// <param name="addressableScene">AssetReference of the scene to load.</param>
        public void QueueLoadScene(AssetReference addressableScene)
        {
            if (_loading)
                _loadQueue.Enqueue(() => { StartLoadingScene(addressableScene); });
            else
                StartLoadingScene(addressableScene);
        }

        /// <summary>
        /// Queues reload of the currently loaded scene or reloads it immediately if no other scene is being loaded.
        /// Checks if another scene is currently being loaded. If yes, an addressable scene load call is added to a
        /// queue, otherwise the scene starts loading.
        /// </summary>
        public void QueueReloadScene()
        {
            if (_lastLoadedScene != null)
            {
                if (_loading)
                    _loadQueue.Enqueue(() => { StartLoadingScene(_lastLoadedScene); });
                else
                    StartLoadingScene(_lastLoadedScene);
            }
            else
            {
                Logger.OnLog("Cannot reload a scene! No scene was loaded using Addressables yet.");
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
        /// Starts an asynchronous operation, which loads a scene, waits until it finishes and invokes a method for
        /// finishing loading a scene.
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
        /// Starts an asynchronous operation, which loads an addressable scene, waits until it finishes and invokes a
        /// a method for finishing loading a scene.
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
        /// First, an after-load event is invoked. If there are other scene load calls in the queue, the first one is
        /// invoked. This happens when a scene load was requested while another scene was still being loaded.
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