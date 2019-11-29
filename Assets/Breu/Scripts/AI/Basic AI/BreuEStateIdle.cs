using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuEStateIdle : EnemyState
    {


        public override EnemyState Update()
        {
            Debug.Log("idling");

            Vector3 disToTarget = Enemy.transform.position - Enemy.Target.position;

            if(disToTarget.sqrMagnitude < Enemy.PursueDistanceThreshold * Enemy.PursueDistanceThreshold)
            {
                return new BreuEStatePursue();
            }

            return null;
        }
    }
}