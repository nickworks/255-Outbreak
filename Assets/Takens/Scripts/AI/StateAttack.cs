using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// 3 Unique attacks of the enemy,
    /// lazer, orbit, and spin attack
    /// </summary>
    public enum AttackType {lazer, orbit, spin}


    /// <summary>
    /// State for when the enemy is attacking
    /// </summary>
    public class StateAttack : EnemyState
    {
        /// <summary>
        /// The wait period between each shot in seconds
        /// </summary>
        float timeBetweenShots = 0.5f;

        /// <summary>
        /// the current wait period for the next shot in seconds
        /// </summary>
        float timeUntilNextShot = 0f;

        /// <summary>
        /// the Current weapon the enemy is using
        /// </summary>
        AttackType currentAttack = AttackType.lazer;

        /// <summary>
        /// the maximum number of bullets the enemy can shoot before having to reload
        /// </summary>
        int ammoMax = 5;

        /// <summary>
        /// the current number of bullets
        /// </summary>
        int ammo = 0;


            /// <summary>
            /// Called once when switching to attack state
            /// </summary>
            /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            
            //generate a random number and use it to determine a random weapon
            int r = UnityEngine.Random.Range(0, 3);
            switch (r) {
                case (0):
                    currentAttack = AttackType.lazer;
                    ammoMax = 14;
                    timeBetweenShots = .3f;
                    break;
                case (1):
                    currentAttack = AttackType.orbit;
                    ammoMax = 2;
                    timeBetweenShots = 3f;
                    break;
                case (2):
                    currentAttack = AttackType.spin;
                    timeBetweenShots = .02f;
                    ammoMax = 100;
                    break;
                default:
                    break;
            }
            ammo = ammoMax;

        }

        /// <summary>
        /// Called once per frame while the enemy is in the ATTACK state
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {

            ////////// BEHAVIOR


            timeUntilNextShot -= Time.deltaTime;

            switch (currentAttack)
            {
                case AttackType.lazer:

                    if (timeUntilNextShot <= 0 && ammo > 0)
                    {
                        ammo--;
                        enemy.ShootLazer();
                        timeUntilNextShot = timeBetweenShots;
                    }

                    break;
                case AttackType.orbit:
                    if (timeUntilNextShot <= 0 && ammo > 0)
                    {
                        ammo--;
                        enemy.ShootOrbit();
                        timeUntilNextShot = timeBetweenShots;
                    }
                    break;
                case AttackType.spin:
                    if (timeUntilNextShot <= 0 && ammo > 0)
                    {
                        ammo--;
                        enemy.ShootSpin();
                        timeUntilNextShot = timeBetweenShots;
                    }
                    break;
                default:
                    break;
            }
           


            ////////// TRANSITIONS TO OTHER STATES

            //transition: if distance > attack threshold, switch to pursue
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;
            float disSqr = toAttackTarget.sqrMagnitude;

            if(disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }


            //transition: if ammo == 0, switch to reload
            if(ammo <= 0)
            {
                return new StateReload();

            }



            return null;
        }
    }
}