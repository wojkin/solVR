using UnityEngine;

namespace Robots.Tankbot
{
    /// <summary>
    /// Class representing a bullet that can be shot.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region Variables

        /// <summary>Time after which the bullet will be destroyed.</summary>
        private const float TimeInScene = 10f;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initiates the destruction of the bullet.
        /// </summary>
        private void Awake()
        {
            Destroy(gameObject, TimeInScene);
        }

        #endregion

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