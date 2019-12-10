using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// State for lunging (melee) attack
    /// </summary>
    public class StateLunge : EnemyState
    {
        /// <summary>
        /// Is lunging or not
        /// </summary>
        public bool lunging = false;
        /// <summary>
        /// Time it takes for lunge animation
        /// </summary>
        public float timeToLunge = 40;

        /// <summary>
        /// Called when state begins
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
        }

        /// <summary>
        /// Called every frame.
        /// Keep lunging at target until it's a certain distance away.
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;
            float disSqr = toAttackTarget.sqrMagnitude;

            if (!lunging)
            {
                timeToLunge = 40;
                lunging = true;
            }
            else
            {
                timeToLunge -= 1;
            }

            if (timeToLunge <= 0)
            {
                if (toAttackTarget.magnitude < 50)
                {
                    DamageTaker dt = enemy.attackTarget.GetComponent<DamageTaker>();
                    if (dt != null)
                        dt.TakeDamage(10); // hurt the thing we hit...
                }
                lunging = false;
            }

            enemy.animation.Play("attack1");

            // transition: if distance > attack threshold and not lunging, switch to pursue

            if (disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold && !lunging)
                return new StatePursue();

            return null;
        }
    }
}