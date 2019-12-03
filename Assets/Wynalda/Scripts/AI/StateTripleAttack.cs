using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class StateTripleAttack : EnemyState
    {
        float timeBetweenShots = 0.5f;
        float timeUntilNextShot = 0;

        int ammo = 0;
        int ammoMax = 15;

        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            ammo = ammoMax;
        }

        public override EnemyState Update()
        {
            ///////// BEHAVIOUR

           // Debug.Log("attacking...");

            timeUntilNextShot -= Time.deltaTime;

           if(timeUntilNextShot <= 0 && ammo > 0)
            {
                ammo--;
                enemy.ShootTripleProjectile();
                timeUntilNextShot = timeBetweenShots;
            }
                                

           

            ///////// TRANSITION TO OTHER STATES:

            //switch to pursue
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;
            float disSqr = toAttackTarget.sqrMagnitude;

            if(disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }

            //switch to reload
            if(ammo <= 0)
            {
                return new StateReload();
            }
       

            return null;
        }
    }
}