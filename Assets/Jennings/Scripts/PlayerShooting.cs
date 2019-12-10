using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings { 
public class PlayerShooting : MonoBehaviour
{
        // Enum for what weapon uses what index number
        public enum WeaponType {
            PeaShooter, // 0
            AutoRifle, // 1
            TripleShot, // 2
            RocketLauncher // 3
        }
    
        public GameObject basicBullet; // Calls basicBullet game object
        public GameObject basicRocket; // calls basicRocket game object
        public Transform projectileSpawnPoint; // determines where in game space projectile spawns
        public WeaponType currentWeapon = WeaponType.PeaShooter; // determines weapon type

        float cooldownUntilNextBullet = 0;
        // Whatever out previous cycle axis state was
        int previousCycleDir = 0;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            CycleWeapons();

            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1")) Shoot();
        }
        // Switch and establish weapon
        private void CycleWeapons()
        {

            float cycleInput = Input.GetAxisRaw("CycleWeapons");

            int cycleDir = 0;
            if (cycleInput < 0) cycleDir = -1;
            if (cycleInput > 0) cycleDir = 1;

            // Cycles through the weapons in Enum
            if (previousCycleDir == 0)
            { // only change weapons if we WEREN'T trying to change weapons last frame

                int index = (int)currentWeapon + cycleDir;

                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0) index = max;
                if (index > max) index = 0;

                currentWeapon = (WeaponType)index;
            }
            previousCycleDir = cycleDir;
        }

        void Shoot()
        {
            // Switches the weapon
            switch (currentWeapon)
            {
                case WeaponType.PeaShooter:     ShootPeaShooter();  break;
                case WeaponType.AutoRifle:      ShootAutoRifle();   break;
                case WeaponType.TripleShot:     ShootTripleShot();  break;
                case WeaponType.RocketLauncher: ShootRocketLauncher(); break;
            }
        }
        // The default gun, repeatedly press to shoot
        private void ShootPeaShooter()
        {
            if (!Input.GetButtonDown("Fire1")) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
        }
        // Rapidly shoots with no cooldown
        private void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }
        // Scattershots 3 bullets at once
        private void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1")) return; // Must release Fire1 to keep shooting

            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));

        }
        // Shoots the "rocket launcher" bullet which is a big ball that moves slow but does the most damage
        private void ShootRocketLauncher()
        {
            //if (!Input.GetButtonDown("Fire1")) return;

            if (cooldownUntilNextBullet > 0) return;

            Instantiate(basicRocket, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 1.5f;
        }
    }
}
