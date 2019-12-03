using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    public class StateIdle : EnemyState
    {
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

            Debug.Log("I'm idle");


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