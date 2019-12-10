using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class StatePursue : EnemyState
    {
        public override EnemyState Update()
        {
            //Debug.Log("I'm pursuing");

            //move towards the player

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;

            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            /////////// TRANSITIONS TO OTHER STATES:

            float disSqr = disToPlayer.sqrMagnitude;

            // switch to IDLE if player is too far:
            if(disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StateIdle();
            }

            if(disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StateAttack();
            }


            return null;
        }
        
    }
}
