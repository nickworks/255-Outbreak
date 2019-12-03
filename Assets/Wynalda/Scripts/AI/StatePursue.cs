using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class StatePursue : EnemyState
    {
        public override EnemyState Update()
        {
            /////////// BEHAVIOR:

          //  Debug.Log("im pursuing...");

            //move towards the player...
            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;
            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            /////////// TRANSITIONS:
            float disSqr = disToPlayer.sqrMagnitude;
            //switch to IDLE
            if(disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StateIdle();
            }
            //switch to ATTACK
            if(disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StateBasicAttack();
            }

            return null;
        }
    }
}