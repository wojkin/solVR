using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class CustomSceneManager : Singleton<CustomSceneManager>
    {
        public delegate void SceneChange(string sceneName);

        public event SceneChange AfterLoad;
        public event SceneChange BeforeUnload;

        private bool _loading;
        private readonly Queue<Action> _loadQueue = new Queue<Action>();

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
            
            AfterLoad?.Invoke(sceneName);
            if (_loadQueue.Count > 0)
            {
                _loadQueue.Dequeue().Invoke();
            }
            else
            {
                _loading = false;
            }
        }

        private void StartLoadingScene(string sceneName)
        {
            _loading = true;
            BeforeUnload?.Invoke(sceneName);
            StartCoroutine(LoadSceneAsync(sceneName));
        }

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