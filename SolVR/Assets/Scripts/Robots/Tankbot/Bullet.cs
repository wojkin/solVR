using UnityEngine;

namespace Robots.Tankbot
{
    /// <summary>
    /// Class representing a bullet that can be shot.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region Custom Methods

        /// <summary>
        /// Shoots the bullet in it's forward direction.
        /// </summary>
        /// <param name="force">Force applied to the bullet.</param>
        public void Shoot(float force)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        }

        #endregion
    }
}