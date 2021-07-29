using Managers;
using UnityEngine;

namespace Dev
{
    public class DevPreloadScene : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.Find("__app") == null) CustomSceneManager.Instance.QueueLoadScene("_preload");
        }
    }
}