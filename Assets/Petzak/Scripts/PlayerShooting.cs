using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    public class PlayerShooting : MonoBehaviour
    {
        public enum WeaponType
        {
            PeaShooter,
            AutoRifle,
            TripleShot
        }

        public GameObject bullet;
        public Transform projectileSpawnPoint;
        public WeaponType currentWeapon = WeaponType.PeaShooter;

        private float cooldownUntilNextBullet = 0;

        void Start()
        {

        }

        void Update()
        {
            if (cooldownUntilNextBullet > 0)
                cooldownUntilNextBullet -= Time.deltaTime;

            if (Input.GetButton("Fire1"))
                Shoot();    
        }

        void Shoot()
        {
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

        void ShootPeaShooter()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
        }

        void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0)
                return;
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }

        void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;

            float yaw = transform.eulerAngles.y;
            float spread = 10;
            Instantiate(bullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(bullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(bullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));
        }
    }
}