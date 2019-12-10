using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andrea
{
    /// <summary>
    /// Handles player weapons
    /// </summary>
    public class PlayerShooting : MonoBehaviour
    {
        /// <summary>
        /// Enumeration of the weapons in game
        /// </summary>
        public enum WeaponType
        {
            PeaShooter,
            AutoRifle,
            TripleShot,
            BigBoi
        }

        /// <summary>
        /// Array of names for UI
        /// </summary>
        public string[] weaponUINames;

        /// <summary>
        /// A simple bullet for the three shot and auto rifle
        /// </summary>        
        public GameObject basicBullet;

        /// <summary>
        /// A manually detonated grenade launched with a fuse
        /// </summary>
        public GameObject bigBoiBullet;

        /// <summary>
        /// A faster but less damaging projectile for the peashooter
        /// </summary>
        public GameObject zipZapBullet;

        /// <summary>
        /// Reference to the players gun barrel position
        /// </summary>
        public Transform projectileSpawnPoint;

        /// <summary>
        /// Enumeration of the currently equipped weapon
        /// </summary>
        public WeaponType currentWeapon = WeaponType.PeaShooter;

        /// <summary>
        /// Text component for the GUI
        /// </summary>
        public Text weaponUI;

        /// <summary>
        /// Cooldown for player projectile rate of fire
        /// </summary>
        float cooldownUntilNextBullet = 0;

        /// <summary>
        /// The value of the "CycleWeapon" axis on the previous frame.
        /// </summary>
        int previousCycleDir = 0;
        
        /// <summary>
        /// Called upon instantiation
        /// </summary>
        void Start()
        {
            weaponUI.text = weaponUINames[(int)currentWeapon]; 
        }
        
        /// <summary>
        /// Called each frame
        /// </summary>
        void Update()
        {
            CycleWeapons();
            weaponUI.text = weaponUINames[(int)currentWeapon];

            if (cooldownUntilNextBullet > 0)
            {
                cooldownUntilNextBullet -= Time.deltaTime;
            }
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        /// <summary>
        /// Called each frame, handles weapon switching logic
        /// </summary>
        private void CycleWeapons()
        {
            float cycleInput = Input.GetAxisRaw("CycleWeapons");

            int cycleDir = 0;
            if (cycleInput < 0)
            {
                cycleDir = -1;
            }
            if (cycleInput > 0)
            {
                cycleDir = 1;
            }

            if (previousCycleDir == 0) // Change weapons only if we weren't trying to do so last frame.
            {
                int index = (int)currentWeapon + cycleDir;

                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0)
                {
                    index = max;
                }
                else if (index > max)
                {
                    index = 0;
                }


                currentWeapon = (WeaponType)index;
            }

            previousCycleDir = cycleDir;
            
        }

        /// <summary>
        /// Shoots the projectile based on the currently selected weapon
        /// </summary>
        void Shoot()
        {
            switch (currentWeapon)
            {
                case WeaponType.PeaShooter: ShootPeaShooter();
                    break;
                case WeaponType.AutoRifle:  ShootAutoRifle();
                    break;
                case WeaponType.TripleShot: ShootTripleShot();
                    break;
                case WeaponType.BigBoi:     ShootBigBoi();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Semi-auto with no cooldown
        /// </summary>
        private void ShootPeaShooter()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;
                
            Instantiate(zipZapBullet, projectileSpawnPoint.position, transform.rotation);
        }

        /// <summary>
        /// Fully automatic with 300 rpm
        /// </summary>
        private void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0)
                return;
            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = .2f;
        }

        /// <summary>
        /// Shoots 3 projectiles in a 30 degree arc
        /// </summary>
        private void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            float yaw = transform.eulerAngles.y;
            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));

        }

        /// <summary>
        /// Fires one projectile with a 1.5 second cooldown
        /// </summary>
        private void ShootBigBoi()
        {
            if (!Input.GetButtonDown("Fire1") || cooldownUntilNextBullet > 0)
                return;

            Instantiate(bigBoiBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 1.5f;
        }
    }
}