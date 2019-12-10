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
        public GameObject MainBody;
        BreuDamageTake Status;


        public GameObject bullet01Basic;
        public GameObject bullet02ThreeBurst;
        public GameObject bullet03Shotgun;
        public GameObject bullet04ShotFun;
        public GameObject Bullet05Melee;

        public Transform BulletSpawn;

        public WeaponType currentWeapon = WeaponType.Basic;

        [HideInInspector]
        public float CooldownToShoot = 0;

        [HideInInspector]
        public float MaxCooldown;

        /// <summary>
        /// what our "CycleWeapons" axis was on the previous frame
        /// </summary>
        private int PreviousCycleDir = 0;

        void Start()
        {
            Status = MainBody.GetComponent<BreuDamageTake>();
        }

        void Update()
        {
            //if status is not null and current health is greater than zero continue with fire logic
            if (Status != null && Status.CurrentHealth > 0)
            {
                CycleWeapons();//remove this when weapon pickups are implemented

                if (CooldownToShoot > 0)
                {
                    CooldownToShoot -= Time.deltaTime;
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }

            }
        }

        private void CycleWeapons()
        {                        
            int CycleDir = 0;
            
            float CycleInput = Input.GetAxisRaw("CycleWeapons");

            if (CycleInput < 0) CycleDir = -1;
            if (CycleInput > 0) CycleDir = 1;

            if (PreviousCycleDir == 0)//only change weapons if we WEREN'T trying to change weapons last frame
            {
                int index = (int)currentWeapon + CycleDir;
                                
                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0)
                {
                    index = max;
                }
                if (index > max)
                {
                    index = 0;
                }

                currentWeapon = (WeaponType)index;
            }

            PreviousCycleDir = CycleDir;
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
            if (CooldownToShoot > 0) return;

            Instantiate(bullet01Basic, BulletSpawn.position, transform.rotation);
            
            CooldownToShoot = 0.1f;
            MaxCooldown = CooldownToShoot;
        }

        private void ShootThreeBurst()
        {
            if (CooldownToShoot > 0) return;

            Instantiate(bullet02ThreeBurst, BulletSpawn.position, transform.rotation);

            CooldownToShoot = .75f;
            MaxCooldown = CooldownToShoot;
        }

        private void ShootShotgun()
        {
            if (CooldownToShoot > 0) return;

            Instantiate(bullet03Shotgun, BulletSpawn.position, transform.rotation);

            CooldownToShoot = .4f;
            MaxCooldown = CooldownToShoot;
        }

        private void ShootShotFun()
        {
            if (CooldownToShoot > 0) return;
            
            Instantiate(bullet04ShotFun, BulletSpawn.position, transform.rotation);

            CooldownToShoot = 1.25f;
            MaxCooldown = CooldownToShoot;
        }
    }
}