using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for health regeneration
    /// </summary>
    public class BossStateHeal : EnemyState
    {
        float timeBetweenShots = 0.016f; //The amount of time between each heal
        float timeUntilNextShot = 0; //The amount of time remaining until the next heal
        float attackDuration; //The amount of time this state lasts
        float degreesToRotate = 45; //Rotation on the y-axis for visual effect
        float amountToHeal = 2; //The amount of health to be returned to the boss on each heal
        float maxHealth = 750; //The maximum amount of health the boss can have (fix)

        DamageTaker dt; //A reference to the boss' health

        /// <summary>
        /// Overrides the base class to implement a DamageTaker reference and reset the attack duration on start up
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            dt = enemy.GetComponent<DamageTaker>();
            attackDuration = 5;
        }

        /// <summary>
        /// Overrides the base class to perform logic on each frame
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            ///// BEHAVIOR:

            attackDuration -= Time.deltaTime; //Decrement the amount of time left in the state
            timeUntilNextShot -= Time.deltaTime; //Decrement the time until the next heal

            if (timeUntilNextShot <= 0)
                //Time to heal and rotate
            {
                enemy.Rotate(degreesToRotate);
                dt.health += amountToHeal;

                if (dt.health > maxHealth)
                {
                    dt.health = maxHealth; //Using local maxhealth because bugs
                }
                timeUntilNextShot = timeBetweenShots; // reset the time until next heal
            }

            ///// TRANSITIONS:

            //transition: when the heal has satisfied its duration resume pursuit.
            if (attackDuration <= 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}