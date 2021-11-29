using Levels;
using UnityEngine;
using UnityEngine.UI;
using VisualScripting.Execution;

namespace UI
{
    /// <summary>
    /// Class responsible for handling ui to control script execution.
    /// </summary>
    public class UIExecutionControls : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Manager that allows to control the execution of the script.</summary>
        [SerializeField] private ExecutionManager executionManager;

        /// <summary>Button for running or resuming the execution.</summary>
        [SerializeField] private Button run;

        /// <summary>Button for stopping the execution while it's running.</summary>
        [SerializeField] private Button stop;

        /// <summary>Button for pausing the execution while it's running.</summary>
        [SerializeField] private Button pause;

        /// <summary>Button for making next step after execution paused.</summary>
        [SerializeField] private Button nextStep;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            SetOnlyRunInteractable();
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            PersistentLevelData.Instance.LevelLoaded += SetOnlyRunInteractable;
            run.onClick.AddListener(executionManager.ResumeOrRun);
            stop.onClick.AddListener(OnStop);
            pause.onClick.AddListener(OnPause);
            nextStep.onClick.AddListener(executionManager.NextStep);
            executionManager.ExecutionStarted += OnExecutionRunning;
            executionManager.ExecutionResumed += OnExecutionRunning;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            if (PersistentLevelData.Instance != null)
                PersistentLevelData.Instance.LevelLoaded -= SetOnlyRunInteractable;
            run.onClick.RemoveListener(executionManager.ResumeOrRun);
            stop.onClick.RemoveListener(OnStop);
            pause.onClick.RemoveListener(OnPause);
            nextStep.onClick.RemoveListener(executionManager.NextStep);
            executionManager.ExecutionStarted -= OnExecutionRunning;
            executionManager.ExecutionResumed += OnExecutionRunning;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets run button to interactable and the rest to not interactable.
        /// </summary>
        private void SetOnlyRunInteractable()
        {
            run.interactable = true;
            stop.interactable = false;
            pause.interactable = false;
            nextStep.interactable = false;
        }

        /// <summary>
        /// Sets all buttons to not interactable.
        /// </summary>
        private void SetAllNotInteractable()
        {
            run.interactable = false;
            stop.interactable = false;
            pause.interactable = false;
            nextStep.interactable = false;
        }

        /// <summary>
        /// On running execution.
        /// Sets only stop and pause buttons to interactable.
        /// </summary>
        private void OnExecutionRunning()
        {
            run.interactable = false;
            stop.interactable = true;
            pause.interactable = true;
            nextStep.interactable = false;
        }

        /// <summary>
        /// On stop button click action.
        /// Sets all buttons to not interactable.
        /// Calls stopping execution of the script method.
        /// </summary>
        private void OnStop()
        {
            SetAllNotInteractable();
            executionManager.StopExecution();
        }

        /// <summary>
        /// On pause button click action.
        /// Sets only run and nextStep buttons to interactable.
        /// Calls pausing the execution of the script method.
        /// </summary>
        private void OnPause()
        {
            pause.interactable = false;
            stop.interactable = false;
            run.interactable = true;
            nextStep.interactable = true;
            executionManager.Pause();
        }

        #endregion
    }
}