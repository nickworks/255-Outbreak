using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman {
    public class StateAttack : EnemyState
    {
        /// <summary>
        /// Time between bullets fired
        /// </summary>
        float timeBetweenShots = 0.5f;
        /// <summary>
        /// Count Down Timer for when the next shot will be fired
        /// </summary>
        float timeUntilNextShot = 0;
        /// <summary>
        /// How many shots are available
        /// </summary>
        int ammo = 0;
        /// <summary>
        /// Maximum ammount of shots
        /// </summary>
        int ammoMax = 5;
        /// <summary>
        /// Random number that decides what attack will be used
        /// </summary>
        float randomBullet = 0;
        /// <summary>
        /// Starts the Enemy with full ammo
        /// </summary>
        /// <param name="enemy"></param>
        public override void onBegin(EnemyController enemy)
        {
            base.onBegin(enemy);

            ammo = ammoMax;
        }

        public override EnemyState Update()
        {
            //////// BEHAVIOR:
            //Debug.Log("I'm Attacking");

            //TODO: Shoot bullets at target
            randomBullet = Random.Range(1,6);

            timeUntilNextShot -= Time.deltaTime;

            //Shoots Long Bullet
            if (timeUntilNextShot <= 0 && ammo>0 && randomBullet <=2)
            {
                ammo--;
                enemy.ShootLongBullet();
                timeUntilNextShot = timeBetweenShots;
            }
            //Shoots Fast Wide Bullet
            if (timeUntilNextShot <= 0 && ammo > 0 && randomBullet <= 4 && randomBullet >2)
            {
                ammo--;
                enemy.ShootWideBullet();
                timeUntilNextShot = timeBetweenShots;
            }
            //Shoots Slow Huge Bullet
            if (timeUntilNextShot <= 0 && ammo > 0 && randomBullet <=6 && randomBullet >4)
            {
                ammo--;
                enemy.ShootBerserkBullet();
                timeUntilNextShot = timeBetweenShots;
            }
            //////// TRANSITIONS TO OTHER STATES:

            //transition: if distance > attack threshold, switch to pursue
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;

            float disSqr = toAttackTarget.sqrMagnitude;
            //Transition to StatePursue if player is out of attack range
            if(disSqr>enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }
            // transition to StateReload if ammo == 0, switch to reload
            if(ammo <= 0)
            {
                return new StateReload();
            }
            //Transition to StateDeathsDoor if under 1000 hp
            if (enemy.bossBeserk == true)
            {
               // return new StateDeathsDoor();
            }
            //Keep in current state
            return null;
        }//End Update
    }
}
