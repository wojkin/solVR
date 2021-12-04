using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using Robots.Actions;
using Robots.Commands;
using Robots.Enums;
using UnityEngine;
using Utils;

namespace Robots
{
    /// <summary>
    /// Base class for all robots, which can execute commands.
    /// </summary>
    public abstract class Robot : MonoBehaviour, ICommandable
    {
        #region Variables

        /// <summary>Delegate for a thread state change.</summary>
        public delegate void ThreadStateChange(ThreadState changedTo);

        /// <summary>Dictionary containing threads created for this robot, uses thread IDs as keys.</summary>
        private readonly Dictionary<int, RobotThread> _threads = new Dictionary<int, RobotThread>();

        /// <summary>Flag (bool wrapped in an object) showing whether command execution should be paused.</summary>
        protected readonly Wrapper<bool> IsPaused = new Wrapper<bool>(false);

        #endregion

        #region Nested Types

        /// <summary>
        /// Class for independently executing commands on a robot.
        /// </summary>
        private class RobotThread : IDisposable
        {
            #region Variables

            /// <summary>Event called when a thread changes its state.</summary>
            public event ThreadStateChange ThreadStateChanged;

            // <summary>Robot on which the thread should execute commands.</summary>
            private readonly Robot _robot;

            /// <summary>State the thread is currently in.</summary>
            internal ThreadState State;

            /// <summary>Command which is currently being executed.</summary>
            public ICommand CurrentlyExecuting { get; private set; }

            /// <summary>Property showing whether the thread is currently busy.</summary>
            public bool CanExecuteCommands => State == ThreadState.Idle;

            #endregion

            #region Custom Methods

            /// <summary>
            /// Initializes a new thread and sets a handler for its state change event.
            /// </summary>
            /// <param name="robot">Robot on which the thread should operate on.</param>
            /// <param name="stateChangeHandler">Handler for the state change event of this thread.</param>
            public RobotThread(Robot robot, ThreadStateChange stateChangeHandler)
            {
                _robot = robot;
                State = ThreadState.Idle;
                CurrentlyExecuting = null;
                ThreadStateChanged += stateChangeHandler;
            }

            /// <summary>
            /// Changes the threads' state and calls an event.
            /// </summary>
            /// <param name="state">The state to which the threads' state will be changed to.</param>
            private void ChangeStateTo(ThreadState state)
            {
                State = state;
                ThreadStateChanged?.Invoke(state);
            }

            /// <summary>
            /// Starts a coroutine which executes a command.
            /// </summary>
            /// <param name="command">The command which will be executed.</param>
            /// <exception cref="ThreadUnavailableException">Exception thrown when this thread can't execute a command
            /// because it's executing another one or is blocked.</exception>
            public void ExecuteCommand(ICommand command)
            {
                // check if the thread can execute a command
                if (!CanExecuteCommands)
                    throw new ThreadUnavailableException("Cannot execute a command on a busy/blocked thread!");

                // get a thread executing a command of the same type (if it exists)
                var thread = _robot.GetThreadExecutingCommand(command);

                // if no other thread is executing a command of the same type, execute the command
                if (thread == null)
                {
                    _robot.StartCoroutine(ExecuteCommandCoroutine(command));
                }
                else
                {
                    // handler which will be called when the other thread finishes execution
                    void FinishedExecutingHandler(ThreadState state)
                    {
                        // check if the thread has finished execution
                        if (state != ThreadState.Idle)
                            return;

                        State = ThreadState.Idle; // set state without invoking a state change event
                        ExecuteCommand(command);
                        // unsubscribe itself from the event - this handler should be called only once
                        thread.ThreadStateChanged -= FinishedExecutingHandler;
                    }

                    // subscribe the handler to an event which will be called when the other thread finishes execution
                    thread.ThreadStateChanged += FinishedExecutingHandler;
                    ChangeStateTo(ThreadState.Blocked);
                }
            }

            /// <summary>
            /// A coroutine for executing a command.
            /// </summary>
            /// <param name="command">The command which will be executed.</param>
            /// <returns>IEnumerator required for a coroutine.</returns>
            private IEnumerator ExecuteCommandCoroutine(ICommand command)
            {
                CurrentlyExecuting = command;
                ChangeStateTo(ThreadState.Executing);

                yield return command.Execute(_robot);

                CurrentlyExecuting = null;
                ChangeStateTo(ThreadState.Idle);
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Removes all delegates registered to the thread change event.
            /// </summary>
            public void Dispose()
            {
                if (ThreadStateChanged != null)
                    foreach (var d in ThreadStateChanged.GetInvocationList())
                        ThreadStateChanged -= (d as ThreadStateChange);
            }

            #endregion
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Pauses command execution by setting the pause flag to true.
        /// </summary>
        public void Pause()
        {
            IsPaused.Value = true;
        }

        /// <summary>
        /// Resumes command execution by setting the pause flag to false.
        /// </summary>
        public void Resume()
        {
            IsPaused.Value = false;
        }

        /// <summary>
        /// Returns a thread which executes a given command type or null if the command is not being executed.
        /// </summary>
        /// <param name="command">Command whose type will be compared.</param>
        /// <returns>Thread which executes a command of this type or null.</returns>
        private RobotThread GetThreadExecutingCommand(ICommand command)
        {
            var commandType = command.GetType();

            foreach (var thread in _threads.Values)
                if (thread.CurrentlyExecuting != null && commandType == thread.CurrentlyExecuting.GetType())
                    return thread;

            return null;
        }

        /// <summary>
        /// Creates a new thread and returns it's unique ID.
        /// </summary>
        /// <returns>The ID of the thread which was created.</returns>
        public int CreateThread(ThreadStateChange stateChangeHandler)
        {
            var lowestIndex = 0;

            // check if there are any threads, if yes search for the lowest free index not used by those threads
            if (_threads.Count != 0)
            {
                lowestIndex = _threads.Keys.Max() + 1; // set the lowest index to the one after the highest one

                // check if there are any free indexes before the highest one
                for (var i = 0; i < _threads.Keys.Max(); i++)
                    if (!_threads.ContainsKey(i))
                    {
                        lowestIndex = i;
                        break;
                    }
            }

            _threads.Add(lowestIndex, new RobotThread(this, stateChangeHandler));
            return lowestIndex;
        }

        /// <summary>
        /// Deletes a thread.
        /// </summary>
        /// <param name="threadId">ID of the thread to be deleted.</param>
        /// <exception cref="ThreadUnavailableException">Exception thrown when the thread to be deleted is executing a
        /// command or is blocked.
        /// </exception>
        public void DeleteThread(int threadId)
        {
            // if the thread can't be deleted, throw an exception
            if (!_threads[threadId].CanExecuteCommands)
                throw new ThreadUnavailableException("Cannot delete a busy/blocked thread!");

            _threads[threadId].Dispose();
            _threads.Remove(threadId);
        }

        /// <summary>
        /// Returns the state of a thread.
        /// </summary>
        /// <param name="threadId">ID of the thread which state should be returned.</param>
        /// <returns>State of a thread.</returns>
        public ThreadState GetThreadState(int threadId)
        {
            return _threads[threadId].State;
        }

        /// <summary>
        /// Starts a coroutine which executes the command on a given thread.
        /// </summary>
        /// <param name="threadId">Id of a thread the command should be executed on.</param>
        /// <param name="command">The command which will be executed.</param>
        /// <exception cref="ThreadDoesNotExistException">Exception thrown if there is no thread with a given ID.
        /// </exception>
        public void ExecuteCommandOnThread(int threadId, ICommand command)
        {
            if (!_threads.ContainsKey(threadId))
                throw new ThreadDoesNotExistException("Cannot execute a command on a thread that doesn't exist");
            _threads[threadId].ExecuteCommand(command);
        }

        #endregion
    }
}