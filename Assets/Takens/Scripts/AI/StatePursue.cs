using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// State called for chasing the player
    /// </summary>
    public class StatePursue : EnemyState
    {
        /// <summary>
        /// Overriden update method that is called every frame
        /// Handles behavior and state transitions
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            ///////// BEHAVIOR
            Debug.Log("pursuing...");


            //move towards the player...
            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;

            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            //////// TRANSITIONS TO OTHER STATES
            float disSqr = disToPlayer.sqrMagnitude;

            //transition: switch to Heal if the enemy is below 50 health
            if(enemy.gameObject.GetComponent<BossHealth>().health < 50)
            {
                return new StateHeal();
            }

            //transition: switch to IDLE if player is too far
            if(disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold && !(enemy.GetComponent<BossHealth>().health >= 100))
            {
                return new StateIdle();
            }

            // transition: switch to ATTACK if player is close enough
            if(disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StateAttack();
            }

            return null;
        }
    }
}