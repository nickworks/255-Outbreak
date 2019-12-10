using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    public class StatePursue : EnemyState
    {
        float timeBetweenShots = 0.5f;
        float timeUntilNextShot = 0;

        public override EnemyState Update()
        {
            //////////////  BEHAVIOR:
            
            timeUntilNextShot -= Time.deltaTime;

            if (timeUntilNextShot <= 0)
            {
                enemy.ShootProjectile();
                timeUntilNextShot = timeBetweenShots;
            }

            // move towards the player...

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;
            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            enemy.transform.LookAt(enemy.attackTarget.position);
            enemy.animation.Play("run");

            ////////////// TRANSITIONS TO OTHER STATES:

            float disSqr = disToPlayer.sqrMagnitude;

            // transition: switch to IDLE if player is too far
            if (disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
                return new StateIdle();

            // transition: switch to ATTACK if the player is close
            if (disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
                return new StateAttack();

            return null;
        }
    }
}