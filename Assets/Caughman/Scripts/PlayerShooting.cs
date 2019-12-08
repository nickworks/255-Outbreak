using System.Collections;
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
            PeaShooter,
            AutoRifle,
            TripleShot
        }

        /// <summary>
        /// Our Bullet Prefab
        /// </summary>
        public GameObject basicBullet;
        public Transform projectileSpawnPoint;
        /// <summary>
        /// State machine to tell what weapons is being used
        /// </summary>
        public Weapontype currentWeapon = Weapontype.PeaShooter;

        float cooldownUntilNextBullet = 0;

        // Start is called before the first frame update
        void Start()
        {

        }//End Start

        // Update is called once per frame
        void Update()
        {
            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1")) Shoot();
        }//End Update

        void Shoot()
        {
            switch (currentWeapon)
            {
                //Weapons State for PeaShooter
                case Weapontype.PeaShooter:
                    ShootPeashooter();
                    break;
                //Weapons State for AutoRifle
                case Weapontype.AutoRifle:
                    ShootAutoRifle();
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

        private void ShootAutoRifle()
        {
            if (cooldownUntilNextBullet > 0) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }//End AutoRifle

        private void ShootTripleShot()
        {
            if (!Input.GetButtonDown("Fire1")) return;//Must release Fire1 to keep shooting

            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw-spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));

        }//End TripleShot
    }
}
