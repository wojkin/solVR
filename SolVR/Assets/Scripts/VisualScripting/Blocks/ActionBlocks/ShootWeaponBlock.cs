using Robots.Commands;

namespace VisualScripting.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a shoot weapon block responsible for creating <see cref="ShootWeaponCommand"/>s.
    /// </summary>
    public class ShootWeaponBlock : Block, IGetCommand
    {
        #region IGetCommand Methods

        /// <summary>
        /// Creates and returns a new <see cref="ShootWeaponCommand"/>.
        /// </summary>
        /// <returns>New <see cref="ShootWeaponCommand"/>.</returns>
        public ICommand GetCommand()
        {
            return new ShootWeaponCommand();
        }

        #endregion
    }
}