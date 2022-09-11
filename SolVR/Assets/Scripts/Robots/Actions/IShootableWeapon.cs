using System.Collections;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can shoot it's weapon.
    /// </summary>
    public interface IShootableWeapon : ICommandable
    {
        #region Custom Methods

        /// <summary>
        /// A coroutine for shooting robot's weapon.
        /// </summary>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        IEnumerator ShootWeapon();

        #endregion
    }
}