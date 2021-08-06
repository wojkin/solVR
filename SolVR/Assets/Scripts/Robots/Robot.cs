using Robots.Actions;
using Robots.Commands;
using UnityEngine;

namespace Robots
{
    /// <summary>
    /// An enum representing the state in which the robot is.
    /// </summary>
    public enum State
    {
        Idle,
        Executing
    }

    /// <summary>
    /// A base class for all robots, which can execute commands.
    /// </summary>
    public class Robot : MonoBehaviour, ICommandable
    {
        private State _state; // a state of the robot 

        public delegate void StateChange(); // a delegate for a state change

        public event StateChange StartedExecuting; // an event called when the robot starts executing a command
        public event StateChange FinishedExecuting; // an event called the robot finishes executing a command

        /// <summary>
        /// Executes the command.
        /// Executes a given command, changes the robots' state and invokes events before and after executing it.
        /// </summary>
        /// <param name="command">The command which will be executed.</param>
        public void ExecuteCommand(ICommand command)
        {
            StartedExecuting?.Invoke();
            _state = State.Executing;
            command.Execute(this);
            _state = State.Idle;
            FinishedExecuting?.Invoke();
        }
    }
}