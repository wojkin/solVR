using System;
using System.Collections;
using System.Collections.Generic;
using Robots.Actions;
using Robots.Commands.Helpers;
using Robots.Enums;
using UnityEngine;

namespace Robots.Tankbot
{
    /// <summary>
    /// A class representing a six wheeled robot which can move turn with a turret which can turn and shoot.
    /// </summary>
    public class Tankbot : Robot, IMovable, ITurnable, IRotatableWeapon, IShootableWeapon
    {
        #region Serialized Fields

        /// <summary>Array containing robots' front wheels.</summary>
        [SerializeField] private WheelCollider[] frontWheels;

        /// <summary>Array containing robots' rear wheels.</summary>
        [SerializeField] private WheelCollider[] middleWheels;

        /// <summary>Array containing robots' rear wheels.</summary>
        [SerializeField] private WheelCollider[] rearWheels;

        /// <summary>Turret on top of the robot.</summary>
        [SerializeField] private Turret turret;

        #endregion

        #region Variables

        ///<summary>Speed at which wheels change steer angle in degrees per second.</summary>
        private const float TurnSpeed = 180f;

        ///<summary>Tolerance for reaching the target steer angle.</summary>
        private const float AngleTolerance = 0.001f;

        #endregion

        #region IMovable Methods

        /// <summary>
        /// A coroutine for moving a robot by applying torque to its' middle wheels.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator Move(float time, float torque)
        {
            var prevTorques = new Queue<float>();

            // store current motor torque values and set the new ones
            foreach (var wheelCollider in middleWheels)
            {
                prevTorques.Enqueue(wheelCollider.motorTorque);
                wheelCollider.motorTorque = torque;
            }

            // wait for a given amount of seconds
            yield return new PausableWaitForSeconds(time, IsPaused);

            // restore previous motor torque values
            foreach (var wheelCollider in middleWheels) wheelCollider.motorTorque = prevTorques.Dequeue();
        }

        #endregion

        #region IRotatableWeapon Methods

        /// <summary>
        /// A coroutine for turning a turret to an angle.
        /// </summary>
        /// <param name="angle">Target angle for the turret.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator RotateWeapon(float angle)
        {
            yield return turret.Rotate(angle);
        }

        #endregion

        #region IShootableWeapon Methods

        /// <summary>
        /// A coroutine for shooting a bullet out of the turret.
        /// </summary>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator ShootWeapon()
        {
            turret.Shoot();
            yield return new WaitForFixedUpdate();
        }

        #endregion

        #region ITurnable Methods

        /// <summary>
        /// A coroutine for turning the robots front and rear wheels in a direction to an angle.
        /// </summary>
        /// <param name="direction">The direction in which the robot's wheels should turn.</param>
        /// <param name="angle">The steer angle of the wheel colliders around the local vertical axis.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator Turn(TurnDirection direction, int angle)
        {
            // calculate the target signed angle
            var targetAngle = direction == TurnDirection.Left ? -angle : angle;
            // calculate the sign for changing the angle (based on whether it should increase or decrease)
            var directionSign = frontWheels[0].steerAngle < targetAngle ? 1f : -1f;

            // change the wheel's steer angle until it reaches the target angle
            while (Math.Abs(frontWheels[0].steerAngle - targetAngle) > AngleTolerance)
            {
                var angleChange = Mathf.Min(TurnSpeed * Time.fixedDeltaTime,
                                      Mathf.Abs(targetAngle - frontWheels[0].steerAngle)) *
                                  directionSign;
                // change the steer angle based on speed, but not overshoot
                foreach (var wheel in frontWheels)
                    wheel.steerAngle += angleChange;
                foreach (var wheel in rearWheels)
                    wheel.steerAngle -= angleChange; // rear wheels have to turn in the opposite direction

                // wait for the next fixed update (so the wheel collider updates itself based on the new value)
                yield return new WaitForFixedUpdate();
            }
        }

        #endregion
    }
}