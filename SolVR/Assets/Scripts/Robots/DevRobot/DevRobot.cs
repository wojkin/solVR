using System.Collections;
using System.Collections.Generic;
using Robots.Actions;
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

            yield return new WaitForSeconds(time); // wait for a given amount of seconds

            // restore previous motor torque values
            foreach (var wheelCollider in rearWheels)
            {
                wheelCollider.motorTorque = prevTorques.Dequeue();
            }
        }

        /// <summary>
        /// A coroutine for turning a robot by applying torque to its' wheels and rotating them.
        /// Sets front wheels steer angle to a given value and moves the robot. Stops moving and changes back the steer
        /// angle to the previous values after a given amount of seconds.
        /// </summary>
        /// <param name="time">The number of seconds the robot should turn for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <param name="direction">The direction in which the robot should turn.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        public IEnumerator Turn(float time, float torque, TurnDirection direction)
        {
            var prevAngles = new Queue<float>();

            // store current steer angle values and set the new ones
            foreach (var wheelCollider in frontWheels)
            {
                prevAngles.Enqueue(wheelCollider.steerAngle);

                if (direction == TurnDirection.Left)
                    wheelCollider.steerAngle = turnAngle;
                else
                    wheelCollider.steerAngle = -turnAngle;
            }
            
            yield return StartCoroutine(Move(time, torque)); // move the robot for a given amount of seconds

            // restore previous steer angle values
            foreach (var wheelCollider in frontWheels)
            {
                wheelCollider.steerAngle = prevAngles.Dequeue();
            }
        }
    }
}