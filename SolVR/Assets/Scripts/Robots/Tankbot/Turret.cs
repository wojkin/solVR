using System;
using System.Collections;
using UnityEngine;

namespace Robots.Tankbot
{
    public class Turret : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Bullet prefab instantiated when the turret shoots.</summary>
        [SerializeField] private GameObject bulletPrefab;

        /// <summary>Spawn point for new bullets.</summary>
        [SerializeField] private Transform bulletSpawnPoint;

        #endregion

        #region Variables

        /// <summary>Force applied to bullets when shooting.</summary>
        private const float BulletForce = 10f;
        /// <summary>Rotation speed of the turret in degrees per second.</summary>
        private const float RotationSpeed = 90f;
        /// <summary>Tolerance for turret angle.</summary>
        private const float AngleTolerance = 0.001f;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Coroutine for rotating the turret to an angle.
        /// </summary>
        /// <param name="targetAngle">Angle the turret will be rotated to. Negative values represent left direction and
        /// positive right.</param>
        /// <returns><see cref="IEnumerator"/> required for a coroutine.</returns>
        public IEnumerator Rotate(float targetAngle)
        {
            // convert local rotation to angle with negative and positive values for left and right
            var localZRotation = transform.localRotation.eulerAngles.z;
            var currentAngle = localZRotation < 180 ? localZRotation : localZRotation - 360;

            // calculate the sign for changing the angle (based on whether it should increase or decrease)
            float directionSign = currentAngle > targetAngle ? -1 : 1;

            // convert target angle to turret's local rotation
            if (targetAngle < 0)
                targetAngle += 360;

            // rotate the turret until it reaches the target angle
            while (Math.Abs(transform.localRotation.eulerAngles.z - targetAngle) > AngleTolerance)
            {
                // change the steer angle based on speed, but not overshoot
                var angleChange = Mathf.Min(RotationSpeed * Time.deltaTime,
                    Mathf.Abs(transform.localRotation.eulerAngles.z - targetAngle));
                transform.Rotate(Vector3.forward, angleChange * directionSign, Space.Self);
                yield return null;
            }
        }

        /// <summary>
        /// Shoots a bullet out of the turrets barrel.
        /// </summary>
        public void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Shoot(BulletForce);
        }

        #endregion
    }
}