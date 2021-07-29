using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadSceneHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CustomSceneManager.Instance.QueueLoadScene("DevScene");
    }
}