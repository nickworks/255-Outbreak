using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for idle behavior, currently only used before the player enters an area
    /// </summary>
    public class StateIdle : EnemyState
    {
        /// <summary>
        /// Called each frame
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            if (enemy == null) 
            {
                return null; //There is no enemy to control
            }

            if (enemy.attackTarget == null)
            {
                return null; //enemy has nothing it wants to attack
            }
            ///// BEHAVIOR:

            //Debug.Log("I'm idle");


            ///// TRANSITIONS TO OTHER STATES:
            Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;
            if (disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePursue();
            }

            //if player is closer than 10m
            //return new StatePursue();

            return null;
        }
    }
}