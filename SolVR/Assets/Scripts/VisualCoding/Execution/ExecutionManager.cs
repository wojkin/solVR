using System.Collections.Generic;
using Robots;
using Robots.Enums;
using UnityEngine;
using VisualCoding.Blocks;
using VisualCoding.Blocks.ActionBlocks;
using VisualCoding.Blocks.LogicBlocks;

namespace VisualCoding.Execution
{
    /// <summary>
    /// Class responsible for executing all blocks in a scene on a given robot. Assumes that all blocks and connections
    /// are valid.
    /// </summary>
    public class ExecutionManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Robot on which commands will be executed.</summary>
        [SerializeField] private Robot robot;

        #endregion

        #region Variables

        /// <summary>ExecutionState showing whether the execution state.</summary>
        private static ExecutionState _executionState = ExecutionState.NotRunning;

        /// <summary>
        /// Flag showing whether execution should be paused after finishing executing the next or current block.
        /// </summary>
        private static bool _pauseOnNextStep;

        /// <summary>List of execution threads responsible for executing blocks which are connected together.</summary>
        private readonly List<BlockExecutionThread> _executionThreads = new List<BlockExecutionThread>();

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

            ///<summary>
            /// Flag representing whether the thread should finish execution before executing the next block.
            /// </summary> 
            private bool _finishExecution;

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
                _threadId = manager.robot.CreateThread(StateChangeHandler);
                _manager = manager;
                _currentBlock = currentBlock;
            }

            /// <summary>
            /// Executes blocks in a loop until any of the stop conditions is met.
            /// </summary>
            private void ExecuteBlocks()
            {
                while (_currentBlock != null && !_finishExecution)
                {
                    // if the execution manager is paused or an action block is executed the execution stops
                    if (_executionState == ExecutionState.Paused)
                        return;

                    if (!ExecuteBlock())
                        return;
                }

                // if there is no block to execute or the finish execution flag is set this thread is deleted
                DeleteThread();
            }

            /// <summary>
            /// Handles the deleting of this thread.
            /// </summary>
            private void DeleteThread()
            {
                _manager.robot.DeleteThread(_threadId); // delete the robot thread created for this thread
                _manager._executionThreads.Remove(this); // remove itself from the list in the execution manager
            }

            /// <summary>
            /// Handles executing of the current block.
            /// </summary>
            /// <remarks>
            /// If a command block is executed, then the execution will continue, when the robot thread changes its
            /// state to idle.
            /// </remarks>
            /// <returns>A bool showing whether execution should continue.</returns>
            private bool ExecuteBlock()
            {
                // check if the current block is an end block
                if (_currentBlock.GetType() == typeof(EndBlock))
                {
                    _manager.ExitAllThreads();
                    return false;
                }

                // check if the current block is an action block
                if (_currentBlock is IGetCommand actionBlock)
                {
                    var command = actionBlock.GetCommand();
                    _manager.robot.ExecuteCommandOnThread(_threadId, command);
                    return false;
                }

                // if the current block wasn't an end or an action block, then set the current block to the next one
                _currentBlock = _currentBlock.NextBlock();
                PauseIfPauseOnNextStep(); // check if the execution should paused
                return true;
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
                // state change to idle means that the robot finished a command and that block execution should resume
                if (changedTo == ThreadState.Idle)
                {
                    _currentBlock = _currentBlock.NextBlock();
                    PauseIfPauseOnNextStep();
                    ExecuteBlocks();
                }
            }

            /// <summary>
            /// If possible, starts executing blocks.
            /// </summary>
            internal void StartExecutingIfIdle()
            {
                // if the robot thread is not idle it means that it still has a command to execute
                if (_manager.robot.GetThreadState(_threadId) == ThreadState.Idle)
                    ExecuteBlocks();
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
                    break;
            }
        }

        /// <summary>
        /// Stops execution of all threads.
        /// </summary>
        public void ExitAllThreads()
        {
            foreach (var thread in _executionThreads)
                thread.FinishExecution();
            _executionState = ExecutionState.NotRunning;
        }

        /// <summary>
        /// Pauses execution of blocks.
        /// </summary>
        public void Pause()
        {
            if (_executionState == ExecutionState.Running)
            {
                _executionState = ExecutionState.Paused;
                _pauseOnNextStep = false;
                robot.Pause();
                Physics.autoSimulation = false;
            }
        }

        /// <summary>
        /// Initializes execution for each start block in the scene.
        /// </summary>
        private void Run()
        {
            _executionState = ExecutionState.Running;
            var startBlocks = FindObjectsOfType<StartBlock>(); // find all start block components in the scene

            // create an execution thread and initialize execution for each of them
            foreach (var startBlock in startBlocks)
            {
                var thread = new BlockExecutionThread(this, startBlock);
                thread.StartExecutingIfIdle();
                _executionThreads.Add(thread);
            }

            if (startBlocks.Length == 0)
                _executionState = ExecutionState.NotRunning;
        }

        /// <summary>
        /// Resumes execution of blocks.
        /// </summary>
        /// <remarks>The execution threads start executing in the order they were initialized.</remarks>
        private void Resume()
        {
            _executionState = ExecutionState.Running;
            robot.Resume();
            Physics.autoSimulation = true;

            // start block execution for all idle robot threads
            foreach (var handler in _executionThreads)
                handler.StartExecutingIfIdle();
        }

        #endregion
    }
}