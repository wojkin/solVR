using System.Collections;
using System.Collections.Generic;
using Robots.Actions;
using Robots.Commands.Helpers;
using Robots.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Robots.DevRobot
{
    /// <summary>
    /// A class representing a development robot, which has four wheels and can move and turn.
    /// </summary>
    public class DevRobot : Robot, IMovable, ITurnable, ICrashable
    {
        #region Serialized Fields

        /// <summary>Array containing robots' front wheels.</summary>
        [SerializeField] private WheelCollider[] frontWheels;

        /// <summary>Array containing robots' rear wheels.</summary>
        [SerializeField] private WheelCollider[] rearWheels;

        /// <summary>Angle at which the robots' wheels should turn.</summary>
        [SerializeField] private float turnAngle;

        #endregion

        #region Variables

        /// <summary>Unity event, which invokes listeners when car's crashed.</summary>
        private UnityEvent _crashed = new UnityEvent();

        #endregion

        #region ICrashable Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="invokeOnCrush"><inheritdoc/></param>
        public void AddListenerOnCrash(UnityAction invokeOnCrush)
        {
            _crashed.AddListener(invokeOnCrush);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="invokeOnCrush"><inheritdoc/></param>
        public void RemoveListenerOnCrash(UnityAction invokeOnCrush)
        {
            _crashed.RemoveListener(invokeOnCrush);
        }

        /// <summary>
        /// <inheritdoc/> It invokes <see cref="_crashed"/> event.
        /// </summary>
        public void Crash()
        {
            _crashed.Invoke();
        }

        #endregion

        #region IMovable Methods

        /// <summary>
        /// A coroutine for moving a robot by applying torque to its' wheels.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        public IEnumerator Move(float time, float torque)
        {
            var prevTorques = new Queue<float>();

            // store current motor torque values and set the new ones
            foreach (var wheelCollider in rearWheels)
            {
                prevTorques.Enqueue(wheelCollider.motorTorque);
                wheelCollider.motorTorque = torque;
            }

            // wait for a given amount of seconds
            yield return new PausableWaitForSeconds(time, IsPaused);

            // restore previous motor torque values
            foreach (var wheelCollider in rearWheels) wheelCollider.motorTorque = prevTorques.Dequeue();
        }

        #endregion

        #region ITurnable Methods

        /// <summary>
        /// A coroutine for turning the robots wheels in a direction to an angle around the local vertical axis.
        /// </summary>
        /// <param name="direction">The direction in which the robot should turn.</param>
        /// <param name="angle">The steer angle of the wheel colliders around the local vertical axis.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        public IEnumerator Turn(TurnDirection direction, int angle)
        {
            // set front wheels steer angle to a given
            foreach (var wheelCollider in frontWheels)
                if (direction == TurnDirection.Left)
                    wheelCollider.steerAngle = turnAngle;
                else
                    wheelCollider.steerAngle = -turnAngle;

            // wait for the next fixed update (so the wheel collider updates itself based on the new value)
            yield return new WaitForFixedUpdate();
        }

        #endregion
    }
}