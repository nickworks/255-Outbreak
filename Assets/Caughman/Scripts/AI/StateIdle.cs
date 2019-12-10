using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman {
    public class StateIdle : EnemyState
    {

        /// <summary>
        /// Checks if the player is close enought to pursue or if health is under 1000
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            if (enemy == null) return null;
            if (enemy.attackTarget == null) return null;

            //////// BEHAVIOUR:
           // Debug.Log("I'm idling");


            //////// TRANSITIONS TO OTHER STATES:

           Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;
            //Transition to StatePursue when player is close enough 
            if(disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePursue();
            }

        //Transition to StateDeathsDoor if under 1000 hp
            if(enemy.bossBeserk == true)
            {
                //return new StateDeathsDoor();
            }

            //Keep Current State
            return null;
        }
    }

}