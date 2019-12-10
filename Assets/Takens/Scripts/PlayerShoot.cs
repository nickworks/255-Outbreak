using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takens
{
    /// <summary>
    /// Class for handeling the players attacks
    /// </summary>
    public class PlayerShoot : MonoBehaviour
    {
        /// <summary>
        /// Enumerator for the 4 different types of weapons:
        /// Single shot, full auto, shotgun, and boomerang
        /// </summary>
       public enum WeaponType
        {
            SingleShot, //0
            FullAuto,   //1
            Shotgun,    //2
            Boomerang   //3
        }

        /// <summary>
        /// Currently selected weapon
        /// </summary>
        public WeaponType currentWeapon = WeaponType.SingleShot;

        /// <summary>
        /// The object to spawn the projectiles from
        /// </summary>
        public Transform projectileSpawnPoint;

        /// <summary>
        /// The prefab for the single shot bullet
        /// </summary>
        public GameObject bulletOne;

        /// <summary>
        /// The prefab for the full auto bullet
        /// </summary>
        public GameObject bulletTwo;

        /// <summary>
        /// The prefab for the shotgun bullet
        /// </summary>
        public GameObject bulletThree;

        /// <summary>
        /// The prefab for the boomerang
        /// </summary>
        public GameObject boomerang;

        /// <summary>
        /// Reference to the text that displays the current weapon type
        /// </summary>
        public Text t;

        /// <summary>
        /// Ammount of time in seconds until the next bullet can be shot
        /// </summary>
        float cooldownUntilNextBullet = 0;

        /// <summary>
        /// Whatever our "CycleWeapon" axis value was one frame ago.
        /// </summary>
        int previousCycleDirection = 0;

        /// <summary>
        /// This method is called once per frame
        /// </summary>
        void Update()
        {
            CycleWeapons();

            if(cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1"))
                Shoot();
        }

        /// <summary>
        /// This method is called once per frame by the Update() method
        /// handles the changing of weapons
        /// </summary>
       void CycleWeapons()
        {
 
            float cycleInput = Input.GetAxisRaw("CycleWeapons");


            int cycleDirection = 0;
            if (cycleInput < 0) cycleDirection = -1;
            if (cycleInput > 0) cycleDirection = 1;

            if (previousCycleDirection == 0)
            {

                int index = (int)currentWeapon + cycleDirection;


                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0) index = max;
                if (index > max) index = 0;
                currentWeapon = (WeaponType)index;

                
            }
            previousCycleDirection = cycleDirection;
        }

        /// <summary>
        /// Called when the player presses the shoot key and is able to fire
        /// </summary>
        void Shoot()
        {
            switch (currentWeapon) {
                case WeaponType.SingleShot:
                    ShootSingleShot();
                    t.text = "Single Shot";
                    break;
                case WeaponType.FullAuto:
                    ShootFullAuto();
                    t.text = "Full Auto";
                    break;
                case WeaponType.Shotgun:
                    ShootShotgun();
                    t.text = "Shotgun";
                    break;
                case WeaponType.Boomerang:
                    ShootBoomerang();
                    t.text = "Boomerang";
                    break;
            }

        }

        /// <summary>
        /// Called when the player shoots the single shot weapon
        /// </summary>
        private void ShootSingleShot()
        {
            if (cooldownUntilNextBullet > 0) return;
            if (!Input.GetButtonDown("Fire1")) return;
            Instantiate(bulletOne, projectileSpawnPoint.position, transform.rotation);

            cooldownUntilNextBullet = 0.1f;
        }

        /// <summary>
        /// Called when the player shoots the full auto weapon
        /// </summary>
        private void ShootFullAuto()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(bulletTwo, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.07f;
        }

        /// <summary>
        /// Called when the player shoots the shotgun
        /// </summary>
        private void ShootShotgun()
        {
            if (!Input.GetButtonDown("Fire1")) return;
            if (cooldownUntilNextBullet > 0) return;
            float yaw = transform.eulerAngles.y;
            float spread = 10f;

            Instantiate(bulletThree, projectileSpawnPoint.position, Quaternion.Euler(0,yaw + spread,0));
            Instantiate(bulletThree, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + (.3f * spread), 0));
            Instantiate(bulletThree, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - (.3f *spread), 0));
            Instantiate(bulletThree, projectileSpawnPoint.position, Quaternion.Euler(0,yaw - spread,0));
            cooldownUntilNextBullet = 1f;
        }

        /// <summary>
        /// Called when the player shoots the boomerang
        /// </summary>
        private void ShootBoomerang()
        {
            if (cooldownUntilNextBullet > 0) return;
            if (!Input.GetButtonDown("Fire1")) return;
            Instantiate(boomerang, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.15f;
        }
    }
}
