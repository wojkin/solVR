using System.Collections;
using System.Collections.Generic;
using Robots.Actions;
using Robots.Commands.Helpers;
using Robots.Enums;
using UnityEngine;

namespace Robots.DevRobot
{
    /// <summary>
    /// A class representing a development robot, which has four wheels and can move and turn.
    /// </summary>
    public class DevRobot : Robot, IMovable, ITurnable
    {
        [SerializeField] private WheelCollider[] frontWheels; // array containing robots' front wheels
        [SerializeField] private WheelCollider[] rearWheels; // array containing robots' rear wheels

        [SerializeField] private float turnAngle; // angle at which the robots' wheels should turn

        /// <summary>
        /// A coroutine for moving a robot by applying torque to its' wheels.
        /// Sets rear wheels motor torque to a given value and changes it back to the previous value after a given
        /// amount of seconds.
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
            foreach (var wheelCollider in rearWheels)
            {
                wheelCollider.motorTorque = prevTorques.Dequeue();
            }
        }

        /// <summary>
        /// A coroutine for turning the robots wheels in a direction to an angle around the local vertical axis.
        /// Sets front wheels steer angle to a given value and wait for the next fixed update (so the wheel collider
        /// updates itself based on the new value).
        /// </summary>
        /// <param name="direction">The direction in which the robot should turn.</param>
        /// <param name="angle">The steer angle of the wheel colliders around the local vertical axis.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        public IEnumerator Turn(TurnDirection direction, int angle)
        {
            foreach (var wheelCollider in frontWheels)
            {
                if (direction == TurnDirection.Left)
                    wheelCollider.steerAngle = turnAngle;
                else
                    wheelCollider.steerAngle = -turnAngle;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}