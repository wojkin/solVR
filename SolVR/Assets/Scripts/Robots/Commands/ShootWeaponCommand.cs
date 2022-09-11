using System.Collections;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for shooting robot's weapon.
    /// </summary>
    public class ShootWeaponCommand : Command<IShootableWeapon>
    {
        #region Custom Methods

        /// <summary>
        /// A coroutine for executing the shoot weapon command on a robot.
        /// </summary>
        /// <param name="robot">The robot, which weapon will be shot.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        protected override IEnumerator TypedExecute(IShootableWeapon robot)
        {
            yield return robot.ShootWeapon();
        }

        #endregion
    }
}