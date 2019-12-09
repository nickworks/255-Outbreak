﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class PlayerShooting : MonoBehaviour
    {
        /// <summary>
        /// List of weapons that can be used
        /// </summary>
       public enum Weapontype
        {
            PeaShooter,//0
            Flamethrower,//1
            TripleShot//2
        }

        /// <summary>
        /// Our Bullet Prefab
        /// </summary>
        public GameObject basicBullet;
        public GameObject flameBullet;
        public GameObject shotgunBullet;
        public Transform projectileSpawnPoint;
        /// <summary>
        /// State machine to tell what weapons is being used
        /// </summary>
        public Weapontype currentWeapon = Weapontype.PeaShooter;

        float cooldownUntilNextBullet = 0;
        /// <summary>
        /// Whatever our "CycleWeapon" Axis value was one frame ago.
        /// </summary>
        int previousCycleDir = 0;

        // Start is called before the first frame update
        void Start()
        {

        }//End Start

        // Update is called once per frame
        void Update()
        {
            CycleWeapons();

            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1")) Shoot();
        }//End Update

        private void CycleWeapons()
        {

          // bool wantsToSwitch = Input.GetButtonDown("CycleWeapons");

           // if (!wantsToSwitch) return;

            float cycleInput = Input.GetAxisRaw("CycleWeapons");

            int cycleDir = 0;
            if (cycleInput < 0) cycleDir = -1;
            if (cycleInput > 0) cycleDir = 1;

            if (previousCycleDir == 0)
            {
                int index = (int)currentWeapon + cycleDir;

                //if (cycle > 0) index++;
                //if (cycle < 0) index--;

                int max = System.Enum.GetNames(typeof(Weapontype)).Length - 1;

                if (index < 0) index = max;
                if (index > max) index = 0;

                currentWeapon = (Weapontype)index;
            }
            previousCycleDir = cycleDir;
            
        }

        void Shoot()
        {
            switch (currentWeapon)
            {
                //Weapons State for PeaShooter
                case Weapontype.PeaShooter:
                    ShootPeashooter();
                    break;
                //Weapons State for AutoRifle
                case Weapontype.Flamethrower:
                    ShootFlamethrower();
                    break;
                //Weapons State for TripleShot
                case Weapontype.TripleShot:
                    ShootTripleShot();
                    break;
            }
        }//End Shoot

        private void ShootPeashooter()
        {
            if (!Input.GetButtonDown("Fire1")) return;//Must release Fire1 to keep shooting

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
        }//End ShootPeaShooter

        private void ShootFlamethrower()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(flameBullet, projectileSpawnPoint.position, transform.rotation);
            
            cooldownUntilNextBullet = 0.1f;
        }//End AutoRifle

        private void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1")) return;//Must release Fire1 to keep shooting

            if (cooldownUntilNextBullet > 0) return;

            cooldownUntilNextBullet = .5f;

            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(shotgunBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw, 0));
            Instantiate(shotgunBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw-spread, 0));
            Instantiate(shotgunBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));

        }//End TripleShot
    }
}
