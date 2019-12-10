using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    public class StatePursue : EnemyState
    {
        public override EnemyState Update()
        {
            ///// BEHAVIOR:

            Debug.Log("I'm Pursuing");

            // move towards the player

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;

            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            ///// TRANSITIONS:

            float disSqr = disToPlayer.sqrMagnitude;

            // transition: switch to IDLE if player is too far

            if (disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StateIdle();
            }

            // transition: switch to ATTACK if player is close
            if (disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StateAttack();
            }
            return null;
        }
    }
}