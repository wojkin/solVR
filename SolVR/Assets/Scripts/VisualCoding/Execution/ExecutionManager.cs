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
        [SerializeField] private Robot robot; // robot on which commands will be executed

        // a list of execution threads responsible for executing blocks which are connected together
        private readonly List<BlockExecutionThread> _executionThreads = new List<BlockExecutionThread>();

        /// <summary>
        /// An enum responsible for a state of the execution.
        /// </summary>
        private enum ExecutionState
        {
            Running,
            Paused,
            NotRunning
        }

        // ExecutionState showing whether the execution state
        private static ExecutionState _executionState = ExecutionState.NotRunning;

        // a flag showing whether execution should be paused after finishing executing the next or current block
        private static bool _pauseOnNextStep;

        /// <summary>
        /// Initializes execution for each start block in the scene.
        /// Finds all start blocks in the scene, creates an execution thread for each of them and starts execution.
        /// Saves all execution threads to a list. Sets variable responsible for showing the execution state.
        /// </summary>
        private void Run()
        {
            _executionState = ExecutionState.Running;
            var startBlocks = FindObjectsOfType<StartBlock>(); // find all start block components in the scene

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
        /// Calls <c>Run</c> or <c>Resume</c> method based on the state of the execution.
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
        /// Sets variable responsible for showing the execution state.
        /// </summary>
        public void ExitAllThreads()
        {
            foreach (var thread in _executionThreads)
                thread.FinishExecution();
            _executionState = ExecutionState.NotRunning;
        }

        /// <summary>
        /// Pauses execution of blocks.
        /// Sets variables responsible for pausing block execution, pauses the command execution on the robot and stops
        /// physics simulation.
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
        /// Resumes execution of blocks.
        /// Sets a variable responsible for pausing block execution, resumes the command execution on the robot and the
        /// physics simulation. Starts block execution for all threads. The execution threads start executing blocks in
        /// the order they were initialized.
        /// </summary>
        private void Resume()
        {
            _executionState = ExecutionState.Running;
            robot.Resume();
            Physics.autoSimulation = true;

            // start block execution for all idle robot threads
            foreach (var handler in _executionThreads)
                handler.StartExecutingIfIdle();
        }

        /// <summary>
        /// Advances execution of blocks until one of them finishes executing.
        /// Sets a variable responsible for pausing execution after a block finishes execution amd resumes execution.
        /// Blocks start execution in the order they were initialized, so an execution thread which was initialized
        /// first will execute all its non-action blocks before a thread that was initialized later.
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
        /// A class responsible for executing a series of connected blocks.
        /// </summary>
        private class BlockExecutionThread
        {
            private readonly int _threadId; // ID of the robot thread created for this execution thread
            private readonly ExecutionManager _manager; // execution manager, which manages this thread

            private Block _currentBlock; // a block which is currently being executed

            // a flag representing whether the thread should finish execution before executing the next block
            private bool _finishExecution;

            /// <summary>
            /// Initializes a new block execution thread.
            /// Creates a new robot thread, which will execute commands generated by this execution thread and saves its
            /// ID. When creating the robot thread, passes a function for handling a state change of the robot thread.
            /// Initializes all necessary variables.
            /// </summary>
            /// <param name="manager">Execution manager which created this execution thread.</param>
            /// <param name="currentBlock">The block from which the execution should begin.</param>
            public BlockExecutionThread(ExecutionManager manager, Block currentBlock)
            {
                _threadId = manager.robot.CreateThread(StateChangeHandler);
                _manager = manager;
                _currentBlock = currentBlock;
            }

            /// <summary>
            /// Executes blocks in a loop until any of the stop conditions is met.
            /// Executes blocks in a loop. If the execution manager is paused or an action block is executed the
            /// execution stops. If there is no block to execute or the finish execution flag is set this thread is
            /// deleted.
            /// </summary>
            private void ExecuteBlocks()
            {
                while (_currentBlock != null || _finishExecution)
                {
                    if (_executionState == ExecutionState.Paused)
                        return;

                    if (!ExecuteBlock())
                        return;
                }

                DeleteThread();
            }

            /// <summary>
            /// Handles the deleting of this thread.
            /// Deletes the robot thread created for this thread and removes itself from the list of threads in the
            /// execution manager.
            /// </summary>
            private void DeleteThread()
            {
                _manager.robot.DeleteThread(_threadId);
                _manager._executionThreads.Remove(this);
            }

            /// <summary>
            /// Handles executing of the current block.
            /// Checks if the current block is an end block, if yes, finishes execution of all threads and returns
            /// false. Next, checks if the current block is an action block. If yes, then it gets a command, executes
            /// it on the robot and returns false. The execution will continue, when the robot thread changes its state
            /// to idle. If the current block wasn't an end block or an action block, then it sets the current block to
            /// the next one, checks if the execution should pause and returns true.
            /// </summary>
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

                _currentBlock = _currentBlock.NextBlock();
                PauseIfPauseOnNextStep();
                return true;
            }

            /// <summary>
            /// Pauses the execution manager if the pause on next step flag is set.
            /// Checks if the flag for pausing on next step is set. If yes, pauses the execution manager and sets the
            /// flag to false.
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
            /// If the robot changes it's state to idle it means that it finished executing a command and that block
            /// execution should be resumed. Sets the current block to the next one, checks if the game should be paused
            /// and starts execution of blocks.
            /// </summary>
            /// <param name="changedTo">State to which the robot thread changed to.</param>
            private void StateChangeHandler(ThreadState changedTo)
            {
                if (changedTo == ThreadState.Idle)
                {
                    _currentBlock = _currentBlock.NextBlock();
                    PauseIfPauseOnNextStep();
                    ExecuteBlocks();
                }
            }

            /// <summary>
            /// If possible, starts executing blocks.
            /// Checks if the robot thread is idle (if it's not it means that an action block is currently being
            /// executed). If yes, starts block execution.
            /// </summary>
            internal void StartExecutingIfIdle()
            {
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
        }
    }
}