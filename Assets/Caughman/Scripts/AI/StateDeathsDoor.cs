using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public abstract class StateDeathsDoor : EnemyState
    {
        /// <summary>
        /// time in seconds it takes to fire another shot
        /// </summary>
        float timeBetweenShots = 0.25f;
        /// <summary>
        /// Time left until next shot
        /// </summary>
        float timeUntilNextShot = 0;

        public override void onBegin(EnemyController enemy)
        {
            base.onBegin(enemy);

            
        }

        public override EnemyState Update()
        {
            //////// BEHAVIOR:
            //Debug.Log("I'm Attacking");

            //TODO: Shoot bullets at target and run towards them

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;
            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            timeUntilNextShot -= Time.deltaTime;
            if (timeUntilNextShot <= 0)
            {
             
                enemy.ShootBerserkBullet();
                timeUntilNextShot = timeBetweenShots;
            }
            //Keep Current State
            return null;
        }
}
}
