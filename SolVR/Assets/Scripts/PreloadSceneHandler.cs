using Managers;
using UnityEngine;

/// <summary>
/// This class should be attached to an object in the _preload scene. It acts as a sign that a _preload scene was
/// already loaded, by naming the object its attached to as an "__app". It handles loading another scene.
/// </summary>
public class PreloadSceneHandler : MonoBehaviour
{
    /// <summary>
    /// Changes the name of the object it's attached to to "__app", moves itself to DDOL scene and loads another scene.
    /// </summary>
    private void Awake()
    {
        this.name = "__app";
        DontDestroyOnLoad(gameObject);
        CustomSceneManager.Instance.QueueLoadScene("DevScene");
    }
}