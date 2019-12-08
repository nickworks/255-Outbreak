using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings { 
public class PlayerShooting : MonoBehaviour
{
        public enum WeaponType {
            PeaShooter,
            AutoRifle,
            TripleShot
        }
    
        public GameObject basicBullet;
        public Transform projectileSpawnPoint;
        public WeaponType currentWeapon = WeaponType.PeaShooter;

        float cooldownUntilNextBullet = 0;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1")) Shoot();
        }
        void Shoot()
        {
            switch (currentWeapon)
            {
                case WeaponType.PeaShooter:     ShootPeaShooter();  break;
                case WeaponType.AutoRifle:      ShootAutoRifle();   break;
                case WeaponType.TripleShot:     ShootTripleShot();  break;
            }
        }

        private void ShootPeaShooter()
        {
            if (!Input.GetButtonDown("Fire1")) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
        }
        private void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }
        private void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1")) return; // Must release Fire1 to keep shooting

            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));

        }
    }
}
