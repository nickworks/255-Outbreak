using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Handles player shooting
    /// </summary>
    public class PlayerShooting : MonoBehaviour
    {
        /// <summary>
        /// Enums for weapon types
        /// </summary>
        public enum WeaponType
        {
            PeaShooter,
            AutoRifle,
            TripleShot
        }

        /// <summary>
        /// Bullet object
        /// </summary>
        public Bullet bullet;
        /// <summary>
        /// Point that the bullet spawns
        /// </summary>
        public Transform projectileSpawnPoint;
        /// <summary>
        /// Current weapon, switches if weapon is picked up
        /// </summary>
        public WeaponType currentWeapon = WeaponType.PeaShooter;
        /// <summary>
        /// Ammo count for rapid or triple shot
        /// </summary>
        public float ammo;
        /// <summary>
        /// Time until next shot
        /// </summary>
        private float cooldownUntilNextBullet = 0;

        /// <summary>
        /// Called on start
        /// </summary>
        void Start()
        {
            bullet.bulletShooter = transform;
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        void Update()
        {
            if (cooldownUntilNextBullet > 0)
                cooldownUntilNextBullet -= Time.deltaTime;

            if (Input.GetButton("Fire1"))
                Shoot();
        }

        /// <summary>
        /// Sets current weapon and shoots bullet
        /// </summary>
        void Shoot()
        {
            currentWeapon = HUD.instance.weapon;
            switch (currentWeapon)
            {
                case WeaponType.PeaShooter:
                    ShootPeaShooter();
                    break;
                case WeaponType.AutoRifle:
                    ShootAutoRifle();
                    break;
                case WeaponType.TripleShot:
                    ShootTripleShot();
                    break;
            }
        }

        /// <summary>
        /// Shoot single shot
        /// </summary>
        void ShootPeaShooter()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
        }

        /// <summary>
        /// Shoot rapid shot
        /// </summary>
        void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0)
                return;
            UpdateAmmo();
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }

        /// <summary>
        /// Shoot spread shot
        /// </summary>
        void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            UpdateAmmo();
            float yaw = transform.eulerAngles.y;
            float spread = 10;
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(bullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(bullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));
        }

        /// <summary>
        /// Update ammo on hud.
        /// Revert to pea shooter if ammo is 0
        /// Pea Shooter has unlimited ammo
        /// </summary>
        void UpdateAmmo()
        {
            HUD.instance.UpdateAmmo();

            if (HUD.instance.ammoCount == 0)
            {
                HUD.instance.UpdateWeapon(WeaponType.PeaShooter);
                currentWeapon = WeaponType.PeaShooter;
            }
        }
    }
}