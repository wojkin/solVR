using Robots.Commands;
using Robots.Enums;
using UnityEngine;
using Utils.ValueInRange;

namespace VisualScripting.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a rotate weapon block responsible for creating <see cref="RotateWeaponCommand"/>s.
    /// </summary>
    public class RotateWeaponBlock : Block, IGetCommand
    {
        #region Serialized Fields
        
        /// <summary>The direction in which the weapon should turn.</summary>
        [SerializeField] [Tooltip("The direction in which the weapon should turn.")]
        private TurnDirection direction;

        /// <summary>Angle to which robot's weapon should be turned.</summary>
        [SerializeField] [Tooltip("Angle to which robot's weapon should be turned.")]
        private ValueInRange<float> angle;

        #endregion

        #region Variables
        
        /// <summary><inheritdoc cref="direction"/></summary>
        public TurnDirection Direction
        {
            get => direction;
            set => direction = value;
        }

        /// <summary><inheritdoc cref="angle"/></summary>
        public float Angle
        {
            get => angle.Value;
            set => angle.Value = value;
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            angle.Initialize();
        }

        #endregion

        #region IGetCommand Methods

        /// <summary>
        /// Creates and returns a new <see cref="RotateWeaponCommand"/>.
        /// </summary>
        /// <returns><see cref="RotateWeaponCommand"/> with angle parameter translated from angle and direction on block
        /// configuration.</returns>
        public ICommand GetCommand()
        {
            float angle360 = Direction == TurnDirection.Left ? -1 * Angle : Angle;
            return new RotateWeaponCommand(angle360);
        }

        #endregion
    }
}