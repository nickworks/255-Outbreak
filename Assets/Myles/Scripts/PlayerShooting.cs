using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Myles
{
    public class PlayerShooting : MonoBehaviour
    {
        public enum WeaponType
        {
            PeaShooter, // 0
            AutoRifle, // 1
            TripleShot, // 2
            Mines // 3

        }

        public Text currentWeaponText;


        public GameObject basicBullet;
        public GameObject mine;
        public Transform projectileSpawnPoint;
        public WeaponType currentWeapon = WeaponType.PeaShooter;


        float cooldownUntilNextBullet = 0;
        int previousCycleDirection = 0;

        void Start()
        {

        }


        void Update()
        {
            CycleWeapons();

            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;

            if (Input.GetButton("Fire1")) Shoot();
        }

        private void CycleWeapons()
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

            currentWeaponText.text = currentWeapon.ToString();
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
                case WeaponType.Mines:
                    LayMines();
                    break;
            }

            print("shooting");

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
            if (!Input.GetButtonDown("Fire1")) return;
            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));
            
        }
        private void LayMines()
        {

            if (cooldownUntilNextBullet > 0) return;

            Instantiate(mine, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 3f;
        }

    }
}
