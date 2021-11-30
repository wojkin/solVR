using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Levels;
using Robots;
using Robots.Commands.Helpers;
using Robots.Enums;
using UnityEngine;
using Utils;
using VisualScripting.Blocks;
using VisualScripting.Blocks.ActionBlocks;
using VisualScripting.Blocks.LogicBlocks;
using VisualScripting.Execution.Enums;
using Logger = DeveloperTools.Logger;

namespace VisualScripting.Execution
{
    /// <summary>
    /// Class responsible for executing all blocks in a scene on a given robot. Assumes that all blocks and connections
    /// are valid.
    /// </summary>
    public class ExecutionManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Parent transform of all blocks.</summary>
        [SerializeField] private Transform blocksRoot;

        #endregion

        #region Variables

        /// <summary>Delegate for a thread step event.</summary>
        public delegate void ThreadStepHandler(int threadId, Block block);

        /// <summary>Delegate for a thread lifecycle event.</summary>
        public delegate void ThreadEventHandler(int threadId);

        /// <summary>Delegate for a thread state change event.</summary>
        public delegate void ThreadStateChangeHandler(int threadId, BlockThreadState state);

        /// <summary>
        /// Delegate for a <see cref="ExecutionManager.ExecutionEnded"/>,
        /// <see cref="ExecutionManager.ExecutionResumed"/> and <see cref="ExecutionManager.ExecutionStarted" /> events.
        /// </summary>
        public delegate void ExecutionHandler();

        /// <summary>Event raised when a <see cref="BlockExecutionThread"/> advances to a new block.</summary>
        public event ThreadStepHandler ThreadStep;

        /// <summary>Event raised when a <see cref="BlockExecutionThread"/> is created.</summary>
        public event ThreadEventHandler ThreadCreated;

        /// <summary>Event raised when a <see cref="BlockExecutionThread"/> is deleted.</summary>
        public event ThreadEventHandler ThreadDeleted;

        /// <summary>Event raised when a <see cref="BlockExecutionThread"/> starts or tops running.</summary>
        public event ThreadStateChangeHandler ThreadStateChanged;

        /// <summary>Event raised when the execution finishes. </summary>
        public event ExecutionHandler ExecutionEnded;

        /// <summary>Event raised when the execution starts. </summary>
        public event ExecutionHandler ExecutionStarted;

        /// <summary>Event raised when the execution resumes. </summary>
        public event ExecutionHandler ExecutionResumed;

        /// <summary>ExecutionState showing whether the execution state.</summary>
        private static ExecutionState _executionState = ExecutionState.NotRunning;

        /// <summary>
        /// Flag showing whether execution should be paused after finishing executing the next or current block.
        /// </summary>
        private static bool _pauseOnNextStep;

        /// <summary>Flag showing whether coroutines executed by the execution manager should be paused.</summary>
        private readonly Wrapper<bool> _pauseCoroutines = new Wrapper<bool>(false);

        /// <summary>List of execution threads responsible for executing blocks which are connected together.</summary>
        private readonly List<BlockExecutionThread> _executionThreads = new List<BlockExecutionThread>();

        /// <summary>Robot on which commands will be executed.</summary>
        private Robot _robot;

        #endregion

        #region Nested Types

        /// <summary>
        /// An enum representing state of execution.
        /// </summary>
        private enum ExecutionState
        {
            Running,
            Paused,
            NotRunning
        }

        /// <summary>
        /// A class responsible for executing a series of connected blocks.
        /// </summary>
        private class BlockExecutionThread
        {
            #region Variables

            /// <summary>ID of the robot thread created for this execution thread.</summary>
            private readonly int _threadId;

            /// <summary>Execution manager, which manages this thread.</summary>
            private readonly ExecutionManager _manager;

            /// <summary>Block which is currently being executed.</summary>
            private Block _currentBlock;

            ///<summary>Flag representing whether the thread should finish execution before executing the next block.</summary> 
            private bool _finishExecution;

            /// <summary>Variable for controlling whether executing blocks in a loop should continue.</summary>
            private bool _executeNextBlock;

            /// <summary>Flag showing whether the thread is waiting because of executing a wait block.</summary>
            private bool _waiting;

            #endregion

            #region Custom Methods

            /// <summary>
            /// Initializes a new block execution thread.
            /// </summary>
            /// <param name="manager">Execution manager which created this execution thread.</param>
            /// <param name="currentBlock">The block from which the execution should begin.</param>
            public BlockExecutionThread(ExecutionManager manager, Block currentBlock)
            {
                // create a new robot thread, which will execute commands generated by this execution thread
                _threadId = manager._robot.CreateThread(StateChangeHandler);
                _manager = manager;
                _currentBlock = currentBlock;
                _manager.ThreadCreated?.Invoke(_threadId); // invoke thread created event
                Logger.Log($"Block execution thread {_threadId} created.");
            }

            /// <summary>
            /// A coroutine for executing blocks in a loop until any of the stop conditions is met.
            /// </summary>
            private IEnumerator ExecuteBlocks()
            {
                while (_currentBlock != null && !_finishExecution)
                {
                    // invoke the thread step event
                    _manager.ThreadStep?.Invoke(_threadId, _currentBlock);

                    // if the execution manager is paused or an action block is executed the execution stops
                    if (_executionState == ExecutionState.Paused)
                        yield break;

                    yield return ExecuteBlock(); // execute current block

                    // command execution can complete and execution can be resumed before this if statement is executed
                    // because of this execute next block flag can't be changed outside of execute block method
                    if (!_executeNextBlock)
                        yield break;
                }

                // if there is no block to execute or the finish execution flag is set this thread is deleted
                DeleteThread();
            }

            /// <summary>
            /// Handles the deleting of this thread.
            /// </summary>
            private void DeleteThread()
            {
                _manager._robot.DeleteThread(_threadId); // delete the robot thread created for this thread
                _manager.DeleteThread(this); // deletes itself in the execution manager
                _manager.ThreadDeleted?.Invoke(_threadId); // invoke thread deleted event
                Logger.Log($"Block execution thread {_threadId} deleted.");
            }

            /// <summary>
            /// Advances execution by one block.
            /// </summary>
            private void AdvanceBlock()
            {
                _currentBlock = _currentBlock.NextBlock();
                PauseIfPauseOnNextStep(); // check if the execution should paused
            }

            /// <summary>
            /// Coroutine for handling the execution of the current block.
            /// </summary>
            /// <remarks>
            /// Because the method will always take at least a frame to execute there is no need to worry about
            /// executing too many blocks in a single frame or stopping execution in the same frame it has begun
            /// (this would remove threads from list while iterating over it).
            /// </remarks>
            /// <returns><see cref="IEnumerator"/> required for the coroutine.</returns>
            private IEnumerator ExecuteBlock()
            {
                switch (_currentBlock)
                {
                    case EndBlock _:
                        // in case of an end block stop execution and set the execute next block flag to true so the t
                        // thread  will be deleted during the next execution loop iteration
                        _manager.StopExecution();
                        _executeNextBlock = true;
                        break;
                    case WaitBlock waitBlock:
                        // in case of a wait block wait and then continue execution
                        _waiting = true;
                        yield return new PausableWaitForSeconds(waitBlock.Time, _manager._pauseCoroutines);
                        _waiting = false;
                        AdvanceBlock();
                        _executeNextBlock = true;
                        break;
                    case IGetCommand actionBlock:
                        // in case of an action block execute the command and stop execution
                        // the execution will resume after the command is completed
                        var command = actionBlock.GetCommand();
                        _manager._robot.ExecuteCommandOnThread(_threadId, command);
                        _executeNextBlock = false;
                        break;
                    default:
                        // in case of any other block continue execution
                        AdvanceBlock();
                        _executeNextBlock = true;
                        break;
                }
            }

            /// <summary>
            /// Pauses the execution manager if the pause on next step flag is set.
            /// </summary>
            private void PauseIfPauseOnNextStep()
            {
                if (_pauseOnNextStep)
                {
                    _manager.Pause();
                    _pauseOnNextStep = false;
                }
            }

            /// <summary>
            /// Handles the robot thread state change.
            /// </summary>
            /// <param name="changedTo">State to which the robot thread changed to.</param>
            private void StateChangeHandler(ThreadState changedTo)
            {
                switch (changedTo)
                {
                    // state change to idle means that the robot finished a command and that block execution should resume
                    case ThreadState.Idle:
                        AdvanceBlock();
                        _manager.StartCoroutine(ExecuteBlocks());
                        break;
                    case ThreadState.Blocked:
                        // invoke thread state change event with the stopped state
                        _manager.ThreadStateChanged(_threadId, BlockThreadState.Stopped);
                        break;
                    case ThreadState.Executing:
                        // invoke thread state change event with the running state
                        _manager.ThreadStateChanged(_threadId, BlockThreadState.Running);
                        break;
                }
            }

            /// <summary>
            /// If possible, starts executing blocks.
            /// </summary>
            internal void StartExecutingIfIdle()
            {
                // if the robot thread is not idle it means that it still has a command to execute
                // don't start execution if it's already running, but waiting because of a wait block
                if (_manager._robot.GetThreadState(_threadId) == ThreadState.Idle && !_waiting)
                    _manager.StartCoroutine(ExecuteBlocks());
            }

            /// <summary>
            /// Sets a flag representing whether the thread should finish execution before executing the next block.
            /// </summary>
            public void FinishExecution()
            {
                _finishExecution = true;
            }

            #endregion
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Subscribes to needed event.
        /// </summary>
        private void OnEnable()
        {
            PersistentLevelData.Instance.LevelLoaded += SetRobot;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed event.
        /// </summary>
        private void OnDisable()
        {
            if (PersistentLevelData.Instance != null) PersistentLevelData.Instance.LevelLoaded -= SetRobot;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Advances execution of blocks until one of them finishes executing.
        /// </summary>
        public void NextStep()
        {
            if (_executionState == ExecutionState.Paused)
            {
                _pauseOnNextStep = true;
                Resume();
            }
        }

        /// <summary>
        /// Calls <see cref="Run"/> or <see cref="Resume"/> method based on the state of the execution.
        /// </summary>
        public void ResumeOrRun()
        {
            switch (_executionState)
            {
                case ExecutionState.NotRunning:
                    Run();
                    break;
                case ExecutionState.Paused:
                    Resume();
                    ExecutionResumed?.Invoke();
                    break;
            }
        }

        /// <summary>
        /// Stops execution of all threads.
        /// </summary>
        public void StopExecution()
        {
            foreach (var thread in _executionThreads)
                thread.FinishExecution();
        }

        /// <summary>
        /// Pauses execution of blocks.
        /// </summary>
        public void Pause()
        {
            if (_executionState == ExecutionState.Running)
            {
                _pauseCoroutines.Value = true;
                _executionState = ExecutionState.Paused;
                _pauseOnNextStep = false;
                _robot.Pause();
                Physics.autoSimulation = false;
            }
        }

        /// <summary>
        /// Resets the state of execution manager so that execution can start again.
        /// </summary>
        private void ResetExecution()
        {
            if (_executionState == ExecutionState.NotRunning) return;
            if (_executionState == ExecutionState.Paused)
            {
                _robot.Resume();
                _pauseCoroutines.Value = false;
            }

            _pauseOnNextStep = false;
            Physics.autoSimulation = true;
            _executionState = ExecutionState.NotRunning;
            ExecutionEnded?.Invoke();
        }

        private void SetRobot() => _robot = PersistentLevelData.Instance.Robot;

        /// <summary>
        /// Initializes execution for each start block in the scene.
        /// </summary>
        private void Run()
        {
            _executionState = ExecutionState.Running;

            // find all start blocks under the blocks root transform
            var startBlocks = new List<StartBlock>();

            foreach (Transform child in blocksRoot)
            {
                var startBlock = child.GetComponent<StartBlock>();
                if (startBlock != null)
                    startBlocks.Add(startBlock);
            }

            // create an execution thread and initialize execution for each of them
            foreach (var startBlock in startBlocks)
            {
                var thread = new BlockExecutionThread(this, startBlock);
                thread.StartExecutingIfIdle();
                _executionThreads.Add(thread);
            }

            if (!startBlocks.Any())
                _executionState = ExecutionState.NotRunning;
            else
                ExecutionStarted?.Invoke();
        }

        /// <summary>
        /// Resumes execution of blocks.
        /// </summary>
        /// <remarks>The execution threads start executing in the order they were initialized.</remarks>
        private void Resume()
        {
            _pauseCoroutines.Value = false;
            _executionState = ExecutionState.Running;
            _robot.Resume();
            Physics.autoSimulation = true;

            // start block execution for all idle robot threads
            foreach (var handler in _executionThreads)
                handler.StartExecutingIfIdle();
        }

        /// <summary>
        /// Deletes a <see cref="BlockExecutionThread"/> and resets execution if it was the last one.
        /// </summary>
        /// <param name="thread"><see cref="BlockExecutionThread"/> to be deleted.</param>
        private void DeleteThread(BlockExecutionThread thread)
        {
            _executionThreads.Remove(thread);

            if (!_executionThreads.Any())
                ResetExecution();
        }

        #endregion
    }
}