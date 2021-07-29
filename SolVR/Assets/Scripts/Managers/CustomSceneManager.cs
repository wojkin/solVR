using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine.SceneManagement;

namespace Managers
{
    /// <summary>
    /// Class for loading and unloading scenes. Keeps a queue of scenes to load, so that when a scene load is
    /// requested before the previous one has finished, they don't collide. Provides events for performing actions before
    /// and after scene load.
    /// </summary>
    public class CustomSceneManager : Singleton<CustomSceneManager>
    {
        public delegate void SceneChange(string sceneName); // a delegate for a scene change (load or unload)

        public event SceneChange AfterLoad; // an event invoked after a scene was loaded
        public event SceneChange BeforeUnload; // an event invoked before a scene start unloading

        private bool _loading; // a flag representing whether a new scene is currently being loaded
        private readonly Queue<Action> _loadQueue = new Queue<Action>(); // a queue of scene load calls

        /// <summary>
        /// A coroutine for loading a scene. After a scene finishes loading an after-load event is invoked. If
        /// after the loading finishes there are other scene load calls in the queue, the first one is invoked. This
        /// happens when a scene load was requested while another scene was still being loaded.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // start loading a scene and wait until it finishes loading 
            var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }

            AfterLoad?.Invoke(sceneName); // invoke the after-load event

            // check if there are other scenes to load, if yes start loading the first one
            if (_loadQueue.Count > 0)
            {
                _loadQueue.Dequeue().Invoke();
            }
            else
            {
                _loading = false;
            }
        }

        /// <summary>
        /// Invokes a before-unload event and start a scene load coroutine.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        private void StartLoadingScene(string sceneName)
        {
            _loading = true;
            BeforeUnload?.Invoke(sceneName);
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        /// <summary>
        /// Checks if another scene is currently being loaded. If yes, a scene load call is added to a queue,
        /// otherwise the scene starts loading.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        public void QueueLoadScene(string sceneName)
        {
            if (_loading)
            {
                _loadQueue.Enqueue(() => { StartLoadingScene(sceneName); });
            }
            else
            {
                StartLoadingScene(sceneName);
            }
        }
    }
}