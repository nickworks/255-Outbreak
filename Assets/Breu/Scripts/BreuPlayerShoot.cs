using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuPlayerShoot : MonoBehaviour
    {

        public enum WeaponType
        {
            Basic,
            ThreeBurst,
            Shotgun,
            Shotfun,
        }

        public GameObject bullet01Basic;
        public GameObject bullet02ThreeBurst;
        public GameObject bullet03Shotgun;
        public GameObject bullet04ShotFun;
        public GameObject Bullet05Melee;

        public Transform BulletSpawn;

        public WeaponType currentWeapon = WeaponType.Basic;

        float CooldownToShoot = 0;

        void Start()
        {

        }

        void Update()
        {
            if (CooldownToShoot > 0)
            {
                CooldownToShoot -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            
        }

        void Shoot()
        {
            switch (currentWeapon)
            {
                case WeaponType.Basic:
                    ShootBasic();
                    break;

                case WeaponType.ThreeBurst:
                    ShootThreeBurst();
                    break;

                case WeaponType.Shotgun:
                    ShootShotgun();
                    break;

                case WeaponType.Shotfun:
                    ShootShotFun();
                    break;
            }
        }

        private void ShootBasic()
        {
            Instantiate(bullet01Basic, BulletSpawn.position, transform.rotation);
        }

        private void ShootThreeBurst()
        {
            if (CooldownToShoot > 0) return;

            Instantiate(bullet02ThreeBurst, BulletSpawn.position, transform.rotation);

            CooldownToShoot = .2f;
        }

        private void ShootShotgun()
        {
            if (CooldownToShoot > 0) return;

            Instantiate(bullet03Shotgun, BulletSpawn.position, transform.rotation);

            CooldownToShoot = .3f;
        }

        private void ShootShotFun()
        {
            if (CooldownToShoot > 0) return;
            
            Instantiate(bullet04ShotFun, BulletSpawn.position, transform.rotation);

            CooldownToShoot = .3f;
        }
    }
}