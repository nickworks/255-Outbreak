using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breu {
    public class BreuEStatePursue : EnemyState
    {

         public override EnemyState Update()
        {
            /////////// behavior

            // move to player :
            Vector3 DisToPlayer = Enemy.Target.position - Enemy.transform.position;
            Vector3 DirToPlayer = DisToPlayer.normalized;

            Enemy.velocity += DirToPlayer * Enemy.Acceleration * Time.deltaTime; //direction to player  *  acceleration  *  time
            ////////////transiftion to other states
            float SqrDis = DisToPlayer.sqrMagnitude;


            // transiton : from PURSUE to IDLE
            if (SqrDis > Enemy.PursueDistanceThreshold * Enemy.PursueDistanceThreshold)
            {
                return new BreuEStateIdle();
            }

            //transition : from PURSUE to ATTACK
            if (SqrDis < Enemy.AttackDistanceThreshold * Enemy.AttackDistanceThreshold)
            {
                return new BreuEStateAttack();
            }

            return null;
        }
    }
}