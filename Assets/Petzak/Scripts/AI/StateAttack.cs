using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Attacking state
    /// </summary>
    public class StateAttack : EnemyState
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
        /// Begins state
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
        }

        /// <summary>
        /// Called every frame
        /// Shoots or changes state
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            //////////////////  BEHAVIOR:

            timeUntilNextShot -= Time.deltaTime;

            if (timeUntilNextShot <= 0)
            {
                enemy.ShootProjectile();
                timeUntilNextShot = timeBetweenShots;
            }

            ////////////////// TRANSITIONS TO OTHER STATES:

            // transition: if distance > attack threshold, switch to pursue

            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;
            float disSqr = toAttackTarget.sqrMagnitude;

            if (disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
                return new StatePursue();

            // transition: if distance > 50, switch to lunge

            if (toAttackTarget.magnitude < 50)
                return new StateLunge();

            return null;
        }
    }
}