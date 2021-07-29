using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class SingleGlobalInstance<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance => _instance;

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}