using Managers;
using UnityEngine;

namespace VisualScripting.Utils
{
    /// <summary>
    /// Component responsible for making sure an object is facing the camera.
    /// </summary>
    public class FaceCamera : MonoBehaviour
    {
        #region Variables

        /// <summary>Multiplier for lerping the rotation of the gameobject.</summary>
        private const float LerpFactor = 0.5f;

        /// <summary>Transform of a camera which should be faced.</summary>
        private Transform _camera;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes the camera transform.
        /// </summary>
        void Start()
        {
            _camera = Camera.main.transform;
        }

        /// <summary>
        /// Smoothly rotates the gameobject to face the camera.
        /// </summary>
        void Update()
        {
            if (GameManager.gameIsPaused) return;

            var targetRot = Quaternion.LookRotation(transform.position - _camera.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, LerpFactor);
        }

        #endregion
    }
}