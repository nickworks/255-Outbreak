using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles { 
public class StateIdle : EnemyState
{
    public override EnemyState Update() {


    if (enemy == null) return null; // there is no enemy to control
    if(enemy.attackTarget == null) return null; // enemy has nohing it wants to attack


    //Debug.Log("I'm idling");
        
       Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;

    if(disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold) {
        return new StatePursue();
        }

        //if player is closer than 10m
        //return new StatePersue();

     return null;

    }
}
} 
