using System.Collections;
using Robots.Actions;
using Robots.Commands;
using Robots.Enums;
using UnityEngine;

namespace Robots
{
    /// <summary>
    /// A base class for all robots, which can execute commands.
    /// </summary>
    public class Robot : MonoBehaviour, ICommandable
    {
        private RobotState _state; // a state of the robot 

        public delegate void StateChange(); // a delegate for a state change

        public event StateChange StartedExecuting; // an event called when the robot starts executing a command
        public event StateChange FinishedExecuting; // an event called the robot finishes executing a command

        /// <summary>
        /// Starts a coroutine which executes the command.
        /// </summary>
        /// <param name="command">The command which will be executed.</param>
        public void ExecuteCommand(ICommand command)
        {
            StartCoroutine(ExecuteCommandCoroutine(command));
        }

        /// <summary>
        /// A coroutine for executing a command.
        /// Executes a given command, changes the robots' state and invokes events before and after executing it.
        /// </summary>
        /// <param name="command">The command which will be executed.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        private IEnumerator ExecuteCommandCoroutine(ICommand command)
        {
            Logger.OnLog("Start executing command!");
            StartedExecuting?.Invoke();
            _state = RobotState.Executing;

            yield return command.Execute(this);

            _state = RobotState.Idle;
            FinishedExecuting?.Invoke();
            Logger.OnLog("Finish executing command!");
        }
    }
}