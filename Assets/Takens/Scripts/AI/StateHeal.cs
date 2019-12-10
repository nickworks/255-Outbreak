using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// State for healing the enemy
    /// </summary>
    public class StateHeal : EnemyState
    {
        /// <summary>
        /// Reference to the health of the enemy
        /// </summary>
        BossHealth health;

        /// <summary>
        /// Overriden begin function is called when switching to heal state
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            //set reference to health
            health = enemy.gameObject.GetComponent<BossHealth>();
            //play green particle effect
            enemy.healer.Play();
        }

        /// <summary>
        /// Called when switching out of this state,
        /// </summary>
        public override void OnEnd()
        {
            base.OnEnd();
            //stop particle effect
            enemy.healer.Stop();
        }

        /// <summary>
        /// Overriden update method that is called every frame
        /// Handles behavior and state transitions
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {

            ////////// BEHAVIOR

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;


            float disSqr = disToPlayer.sqrMagnitude;

            if (disSqr < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {

                //trys to run away from the player but not in the y direction
                enemy.velocity += -dirToPlayer * enemy.acceleration* .4f * Time.deltaTime;
                enemy.velocity.y = 0;

            }

            //add 6 health every second while healing
            health.health += 6 * Time.deltaTime;






            //////// TRANSITIONS TO OTHER STATES


            //transition: switch to PURSUE if the health is full
            if(health.health >= 100)
            {
                health.health = 100;
                return new StatePursue();

            }

            //transition: switch to ATTACK if the player is within range
            if (disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StateAttack();
            }

            return null;
        }
    }
}