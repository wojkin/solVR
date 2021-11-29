using System;
using System.Collections;
using System.Collections.Generic;
using Robots.Actions;
using Robots.Commands.Helpers;
using Robots.Enums;
using UnityEngine;

namespace Robots.Tribot
{
    /// <summary>
    /// A class representing a three wheeled robot which can move and turn.
    /// </summary>
    public class Tribot : Robot, IMovable, ITurnable
    {
        #region Serialized Fields

        /// <summary>Array containing robots' front wheels.</summary>
        [SerializeField] private WheelCollider frontWheel;

        /// <summary>Array containing robots' rear wheels.</summary>
        [SerializeField] private WheelCollider[] rearWheels;

        #endregion

        #region Variables

        ///<summary>Speed at which wheels change steer angle in degrees per second.</summary>
        private const float TurnSpeed = 180f;
        ///<summary>Tolerance for reaching the target steer angle.</summary>
        private const float AngleTolerance = 0.001f;

        #endregion

        #region IMovable Methods

        /// <summary>
        /// A coroutine for moving a robot by applying torque to its' rear wheels.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
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
        /// A coroutine for turning the robots front wheel in a direction to an angle.
        /// </summary>
        /// <param name="direction">The direction in which the robot's wheel should turn.</param>
        /// <param name="angle">The steer angle of the wheel colliders around the local vertical axis.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator Turn(TurnDirection direction, int angle)
        {
            // calculate the target signed angle
            var targetAngle = direction == TurnDirection.Left ? -angle : angle;
            // calculate the sign for changing the angle (based on whether it should increase or decrease)
            var directionSign = frontWheel.steerAngle < targetAngle ? 1f : -1f;

            // change the wheel's steer angle until it reaches the target angle
            while (Math.Abs(frontWheel.steerAngle - targetAngle) > AngleTolerance)
            {
                // change the steer angle based on speed, but not overshoot
                frontWheel.steerAngle +=
                    Mathf.Min(TurnSpeed * Time.fixedDeltaTime, Mathf.Abs(targetAngle - frontWheel.steerAngle)) *
                    directionSign;

                // wait for the next fixed update (so the wheel collider updates itself based on the new value)
                yield return new WaitForFixedUpdate();
            }
        }

        #endregion
    }
}