using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    public class PlayerShoot : MonoBehaviour
    {
       public enum WeaponType
        {
            SingleShot, 
            FullAuto,
            Shotgun,
            Boomerang
        }

        public WeaponType currentWeapon = WeaponType.SingleShot;
        public Transform projectileSpawnPoint;
        public GameObject bulletOne;
        public GameObject boomerang;

        float cooldownUntilNextBullet = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1"))
                Shoot();
        }

        void Shoot()
        {
            switch (currentWeapon) {
                case WeaponType.SingleShot:
                    ShootSingleShot();
                    break;
                case WeaponType.FullAuto:
                    ShootFullAuto();
                    break;
                case WeaponType.Shotgun:
                    ShootShotgun();
                    break;
                case WeaponType.Boomerang:
                    ShootBoomerang();
                    break;
            }

        }

        private void ShootSingleShot()
        {
            if (cooldownUntilNextBullet > 0) return;
            if (!Input.GetButtonDown("Fire1")) return;
            Instantiate(bulletOne, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }
        private void ShootFullAuto()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(bulletOne, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.07f;
        }
        private void ShootShotgun()
        {
            if (!Input.GetButtonDown("Fire1")) return;

            float yaw = transform.eulerAngles.y;
            float spread = 10f;

            Instantiate(bulletOne, projectileSpawnPoint.position, transform.rotation);
            Instantiate(bulletOne, projectileSpawnPoint.position, Quaternion.Euler(0,yaw - spread,0));
            Instantiate(bulletOne, projectileSpawnPoint.position, Quaternion.Euler(0,yaw + spread,0));
        }
        private void ShootBoomerang()
        {
            if (cooldownUntilNextBullet > 0) return;
            if (!Input.GetButtonDown("Fire1")) return;
            Instantiate(boomerang, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.2f;
        }
    }
}
