using System.Collections;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can rotate it's weapon.
    /// </summary>
    public interface IRotatableWeapon : ICommandable
    {
        #region Custom Methods

        /// <summary>
        /// A coroutine for rotating robot's weapon to an angle.
        /// </summary>
        /// <param name="angle">Target angle for the weapon.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        IEnumerator RotateWeapon(float angle);

        #endregion
    }
}