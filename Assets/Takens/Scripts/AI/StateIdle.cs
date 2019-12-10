using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// State for ideling when the player is out of range
    /// </summary>
    public class StateIdle : EnemyState
    {
        /// <summary>
        /// Overriden update method that is called every frame
        /// Handles behavior and state transitions
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {

            if (enemy == null)
                return null;

            if (enemy.attackTarget == null)
                return null;

            ////////// BEHAVIOR

            //do nothing




            //////// TRANSITIONS TO OTHER STATES

            Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;

            //transition: switch to PURSUE if the player is within range
            if (disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePursue();
            }

            //transition: switch to HEAL if the enemy has less than 70 health
            if (enemy.gameObject.GetComponent<BossHealth>().health < 70)
            {
                return new StateHeal();
            }

            return null;
        }
    }
}