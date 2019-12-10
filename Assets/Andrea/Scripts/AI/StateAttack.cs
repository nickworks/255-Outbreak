using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for shooting at the player
    /// </summary>
    public class StateAttack : EnemyState
    {
        float timeBetweenShots = 0.2f; // The rate of fire
        float timeUntilNextShot = 0; // The amount of time until the next shot

        int ammo = 0; // current ammo
        int ammoMax = 15; // full magazine


        /// <summary>
        /// Called when entering the state
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            ammo = ammoMax; //Refills the magazine
        }

        /// <summary>
        /// Called each frame, calls projectile logic, decrements rate of fire timers and ammo count
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            ///// BEHAVIOR:

            timeUntilNextShot -= Time.deltaTime;

            if (timeUntilNextShot <= 0)
            {
                ammo--;
                enemy.ShootProjectile();
                timeUntilNextShot = timeBetweenShots;
            }

            enemy.RotateTowardPlayer(); //Ensure that the enemy is still looking at the player

            ///// TRANSITIONS:

            //transition: if distance > attack threshold, switch to pursue

            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;

            float disSqr = toAttackTarget.sqrMagnitude;

            if (disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }

            // transition: if ammo == 0, switch to reload

            if (ammo <= 0)
            {
                return new StateReload();
            }

            return null;
        }
    }
}