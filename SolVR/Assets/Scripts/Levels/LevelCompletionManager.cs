using System.Collections;
using Managers;
using ScriptableObjects.Environments;
using Tasks;
using UI;
using UnityEngine;
using VisualScripting.Execution;

namespace Levels
{
    /// <summary>
    /// Class responsible for ending level after execution is ended or task is completed/failed. 
    /// </summary>
    public class LevelCompletionManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>The ui menu to display level completion state.</summary>
        [SerializeField] private UILevelCompletion levelCompletionUI;

        /// <summary>Script execution manager that run blocks.</summary>
        [SerializeField] private ExecutionManager executionManager;

        #endregion

        #region Variables

        /// <summary> Delay after execution ended to display level completion.</summary>
        private const float TimeDelay = 2;

        /// <summary>
        /// Task that represent what needs to be completed to complete the level.
        /// </summary>
        private Task _task;

        /// <summary>
        /// The next level that will be accessible after completing this level.
        /// </summary>
        private Level _nextLevel;

        public bool StoppedByUser { get; set; }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            _nextLevel = PersistentLevelData.Instance.nextLevel;
            if (_nextLevel == null)
                levelCompletionUI.DeactivateNextLevelButton(); // set next level button to not active
            else
                levelCompletionUI.AddNextLevelListener(LoadNextLevel); // add loading next level 
        }


        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            PersistentLevelData.Instance.LevelLoaded += InitializeData;
            executionManager.ExecutionEnded += LevelEnded;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            executionManager.ExecutionEnded -= LevelEnded;
            if (PersistentLevelData.Instance != null) PersistentLevelData.Instance.LevelLoaded -= InitializeData;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Load <see cref="_nextLevel"/>.
        /// </summary>
        private void LoadNextLevel()
        {
            PersistentLevelData.Instance.SetLevelData(_nextLevel);
            CustomSceneManager.Instance.QueueLoadLevel(_nextLevel.scene);
        }

        /// <summary>
        /// Initialize task and add stopping the execution on task completed/failed. 
        /// </summary>
        private void InitializeData()
        {
            _task = PersistentLevelData.Instance.Task;
            StoppedByUser = false;
            _task.completed.AddListener(executionManager.StopExecution);
            _task.failed.AddListener(executionManager.StopExecution);
        }

        /// <summary>
        /// Coroutine for displaying level menu after time delay. 
        /// </summary>
        IEnumerator DisplayingStatus()
        {
            // wait for tasks to complete
            yield return new WaitForSeconds(TimeDelay);

            switch (_task.State)
            {
                case TaskCompletionState.Completed:
                    levelCompletionUI.ShowResult(true);
                    break;
                default:
                    levelCompletionUI.ShowResult(false);
                    break;
            }
        }


        /// <summary>
        /// Depends on state of the task shows result menu after time.
        /// </summary>
        private void LevelEnded()
        {
            if (StoppedByUser)
                CustomSceneManager.Instance.QueueReloadScene();
            else
                StartCoroutine(DisplayingStatus());
        }

        #endregion
    }
}