using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    public class StateIdle : EnemyState
    {
        public override EnemyState Update()
        {

            if (enemy == null)
                return null;

            if (enemy.attackTarget == null)
                return null;

            ////////// BEHAVIOR


            Debug.Log("ideling...");




            //////// TRANSITIONS TO OTHER STATES

            Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;


            if(disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}