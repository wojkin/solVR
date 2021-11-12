using System.Collections;
using UnityEngine;
using VisualCoding.Execution.Enums;

namespace VisualCoding.Debugging
{
    /// <summary>
    /// Component for managing a debugging marker which appears above a currently executing block.
    /// </summary>
    public class DebugMarker : MonoBehaviour
    {
        #region Variables

        /// <summary>Distance above the block center where the marker should appear.</summary>
        private const float VerticalMarkerOffset = 0.4f;
        /// <summary>Lerp factor for moving the marker between blocks.</summary>
        private const float MoveLerpFactor = 40f;
        /// <summary>Lerp factor for scaling the marker down before deleting it.</summary>
        private const float ScaleLerpFactor = 10f;
        /// <summary>Scale below which the marker doesn't need to be scaled down and can be destroyed.</summary>
        private const float DestroyScale = 0.01f;
        /// <summary>Color representing the running state.</summary>
        private readonly Color _runningColor = Color.green;
        /// <summary>Color representing the stopped state.</summary>
        private readonly Color _stoppedColor = Color.red;

        /// <summary>Renderer component of the marker.</summary>
        private Renderer _renderer;
        /// <summary>Point light of the marker.</summary>
        private Light _pointLight;
        /// <summary>Transform of the target block.</summary>
        private Transform _targetBlock;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _pointLight = GetComponent<Light>();
        }

        /// <summary>
        /// Smoothly moves the marker to it's target position.
        /// </summary>
        private void Update()
        {
            transform.position =
                Vector3.Lerp(transform.position, OffsetPosition(_targetBlock.position),
                    MoveLerpFactor * Time.deltaTime);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Adds offset to the block's position.
        /// </summary>
        /// <param name="blockPosition">Center of the block.</param>
        /// <returns>Position with offset.</returns>
        private static Vector3 OffsetPosition(Vector3 blockPosition)
        {
            return blockPosition + Vector3.up * VerticalMarkerOffset;
        }

        /// <summary>
        /// Changes the target block above which the marker should appear.
        /// </summary>
        /// <param name="blockTransform">Transform of the target block.</param>
        public void ChangeTargetBlock(Transform blockTransform)
        {
            // if this is the first block move the marker to it's target position instantly
            if (_targetBlock == null)
                transform.position = OffsetPosition(blockTransform.position);

            transform.SetParent(blockTransform);
            _targetBlock = blockTransform;
        }

        /// <summary>
        /// Visualises the state of a block thread execution by changing marker and light color.
        /// </summary>
        /// <param name="state">State to be visualized.</param>
        public void VisualizeState(BlockThreadState state)
        {
            switch (state)
            {
                case BlockThreadState.Running:
                    _renderer.material.color = _runningColor;
                    _pointLight.color = _runningColor;
                    break;
                case BlockThreadState.Stopped:
                    _renderer.material.color = _stoppedColor;
                    _pointLight.color = _stoppedColor;
                    break;
            }
        }

        /// <summary>
        /// Deletes the marker.
        /// </summary>
        public void DeleteMarker()
        {
            StartCoroutine(SmoothDelete());
        }

        /// <summary>
        /// Coroutine which scales the marker down and when it's small enough, deletes it. Lowers the marker's light
        /// intensity along with the scale.
        /// </summary>
        /// <returns><see cref="IEnumerator"/> required for the coroutine.</returns>
        private IEnumerator SmoothDelete()
        {
            // get the initial light intensity
            var lightIntensity = _pointLight.intensity;

            while (transform.localScale.x > DestroyScale)
            {
                // smoothly scale down the marker
                var localScale = transform.localScale;
                localScale = Vector3.Lerp(localScale, Vector3.zero, ScaleLerpFactor * Time.deltaTime);
                transform.localScale = localScale;

                // lower the light intensity proportionally to the scale
                _pointLight.intensity = lightIntensity * localScale.x;

                yield return null;
            }

            Destroy(gameObject);
        }

        #endregion
    }
}