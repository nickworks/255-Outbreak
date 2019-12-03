using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman {
    public class StateAttack : EnemyState
    {
        float timeBetweenShots = 0.5f;
        float timeUntilNextShot = 0;

        int ammo = 0;
        int ammoMax = 5;

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
            timeUntilNextShot -= Time.deltaTime;
            if (timeUntilNextShot <= 0 && ammo>0)
            {
                ammo--;
                enemy.ShootProjectile();
                timeUntilNextShot = timeBetweenShots;
            }

            //////// TRANSITIONS TO OTHER STATES:

            //transition: if distance > attack threshold, switch to pursue
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;

            float disSqr = toAttackTarget.sqrMagnitude;

            if(disSqr>enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }
            // transition: if ammo == 0, switch to reload
            if(ammo <= 0)
            {
                return new StateReload();
            }
            return null;
        }//End Update
    }
}
