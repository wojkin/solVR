using System.Collections.Generic;
using UnityEngine;
using VisualCoding.Blocks;
using VisualCoding.Execution;
using VisualCoding.Execution.Enums;

namespace VisualCoding.Debugging
{
    /// <summary>
    /// Class responsible for visualizing the execution of blocks by managing debug markers.
    /// </summary>
    public class ExecutionVisualizer : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// <see cref="ExecutionManager"/> which execution should be visualized.
        /// </summary>
        [SerializeField]
        private ExecutionManager executionManager;

        /// <summary>
        /// Prefab of a debug marker.
        /// </summary>
        [SerializeField]
        private GameObject debugMarkerPrefab;

        #endregion

        #region Variables

        /// <summary>
        /// Dictionary containing instantiated markers with thread IDs as keys and <see cref="DebugMarker"/> components
        /// as values.
        /// </summary>
        private Dictionary<int, DebugMarker> _debugMarkers;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Awake()
        {
            _debugMarkers = new Dictionary<int, DebugMarker>();
        }

        /// <summary>
        /// Registers handlers for events of the <see cref="executionManager"/>.
        /// </summary>
        private void OnEnable()
        {
            executionManager.ThreadCreated += ThreadCreatedHandler;
            executionManager.ThreadDeleted += ThreadDeletedHandler;
            executionManager.ThreadStep += ThreadStepHandler;
            executionManager.ThreadStateChanged += ThreadStateChangedHandler;
        }

        /// <summary>
        /// Removes handlers for events of the <see cref="executionManager"/>.
        /// </summary>
        private void OnDisable()
        {
            executionManager.ThreadCreated -= ThreadCreatedHandler;
            executionManager.ThreadDeleted -= ThreadDeletedHandler;
            executionManager.ThreadStep -= ThreadStepHandler;
            executionManager.ThreadStateChanged -= ThreadStateChangedHandler;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Handler for the thread created event of the <see cref="executionManager"/>. Creates a new debug marker.
        /// </summary>
        /// <param name="threadId">ID of the new thread.</param>
        private void ThreadCreatedHandler(int threadId)
        {
            var marker = Instantiate(debugMarkerPrefab).GetComponent<DebugMarker>();
            _debugMarkers.Add(threadId, marker);
        }

        /// <summary>
        /// Handler for the thread deleted event of the <see cref="executionManager"/>. Deletes the debug marker
        /// assigned to this thread.
        /// </summary>
        /// <param name="threadId">ID of the deleted thread.</param>
        private void ThreadDeletedHandler(int threadId)
        {
            _debugMarkers[threadId].DeleteMarker();
            _debugMarkers.Remove(threadId);
        }

        /// <summary>
        /// Handler for an event raised when the <see cref="executionManager"/> moves onto a new block. Updates the
        /// target for the marker assigned to this thread.
        /// </summary>
        /// <param name="threadId">ID of the thread which raised the event.</param>
        /// <param name="block">The new block which is being executed.</param>
        private void ThreadStepHandler(int threadId, Block block)
        {
            _debugMarkers[threadId].ChangeTargetBlock(block.transform);
        }

        /// <summary>
        /// Handler for an event raised when the <see cref="executionManager"/> thread starts or tops running.
        /// Updates the marker visualization.
        /// </summary>
        /// <param name="threadId">ID of the thread which changed state.</param>
        /// <param name="state">New state of the thread.</param>
        private void ThreadStateChangedHandler(int threadId, BlockThreadState state)
        {
            _debugMarkers[threadId].VisualizeState(state);
        }

        #endregion
    }
}