using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for chasing after the player
    /// </summary>
    public class StatePursue : EnemyState
    {
        public override EnemyState Update()
        {
            if (enemy.attackTarget == null)
            {
                return null;
            }
            ///// BEHAVIOR:


            // Debug.Log("I'm Pursuing");

            // rotate towards the player
            enemy.RotateTowardPlayer();

            // move towards the player

            Vector3 disToPlayer = enemy.attackTarget.position - enemy.transform.position;
            Vector3 dirToPlayer = disToPlayer.normalized;

            enemy.velocity += dirToPlayer * enemy.acceleration * Time.deltaTime;

            ///// TRANSITIONS:

            float disSqr = disToPlayer.sqrMagnitude;

            // transition: switch to IDLE if player is too far

            if (disSqr > enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePatrol();
            }

            // transition: switch to ATTACK if player is close
            if (disSqr < enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                if (enemy.isBoss)
                    //This enemy is the boss and has opportunity to enter boss only states
                {
                    float randomNumber = UnityEngine.Random.Range(0, 10);
                    if (randomNumber > 8)
                    {
                        return new BossStateSpiralAttack();
                    }
                    if (randomNumber < 2)
                    {
                        return new BossStateHeal();
                    }
                }
                return new StateAttack();
            }
            return null;
        }
    }
}