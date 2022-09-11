using System.Collections;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for rotating robot's weapon.
    /// </summary>
    public class RotateWeaponCommand : Command<IRotatableWeapon>
    {
        #region Variables

        /// <summary>Angle to which robot's weapon should be turned.</summary>
        private readonly float _angle;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Constructor for a rotate weapon command.
        /// </summary>
        /// <param name="angle">Angle to which robot's weapon should be turned.</param>
        public RotateWeaponCommand(float angle)
        {
            _angle = angle;
        }

        /// <summary>
        /// A coroutine for executing the rotate weapon command on the robot.
        /// </summary>
        /// <param name="robot">The robot, which weapon will be rotated.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        protected override IEnumerator TypedExecute(IRotatableWeapon robot)
        {
            yield return robot.RotateWeapon(_angle);
        }

        #endregion
    }
}