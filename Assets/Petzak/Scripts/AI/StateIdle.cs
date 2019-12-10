using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Idle state
    /// Shoots spread and taunts
    /// </summary>
    public class StateIdle : EnemyState
    {
        /// <summary>
        /// Time inbetween shots
        /// </summary>
        float timeBetweenShots = 0.5f;
        /// <summary>
        /// Time until next shot
        /// </summary>
        float timeUntilNextShot = 0;

        /// <summary>
        /// Called every frame
        /// Shoot spread and taunt or switch states
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            if (enemy == null)
                return null; // there is no enemy to control...
            if (enemy.attackTarget == null)
                return null; // enemy has nothing it wants to attack...

            timeUntilNextShot -= Time.deltaTime;

            if (timeUntilNextShot <= 0)
            {
                enemy.ShootSpread();
                timeUntilNextShot = timeBetweenShots;
            }

            /////// BEHAVIOR:

            enemy.animation.Play("taunt");

            /////// TRANSITIONS TO OTHER STATES:

            enemy.transform.LookAt(enemy.attackTarget.position);

            Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;
            if (disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
                return new StatePursue();

            return null;
        }
    }
}