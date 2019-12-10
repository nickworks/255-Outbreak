using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for a spinning volley of projectiles
    /// </summary>
    public class BossStateSpiralAttack : EnemyState
    {
        float timeBetweenShots = 0.1f; //The amount of time between each shot
        float timeUntilNextShot = 0; //The amount of time remaining until the next shot
        float attackDuration; //The amount of time this state lasts
        float degreesToRotate = 15; //Rotation on the y-axis for area of effect


        /// <summary>
        /// Overrides the base class to reset the attack duration on start up
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            attackDuration = 5;
        }
        public override EnemyState Update()
        {
            ///// BEHAVIOR:

            attackDuration -= Time.deltaTime; //Decrement the amount of time left in the state
            timeUntilNextShot -= Time.deltaTime; //Decrement the time until the next heal

            if (timeUntilNextShot <= 0)
                //Time to shoot and rotate
            {
                enemy.ShootProjectileUnaimed(degreesToRotate);
                timeUntilNextShot = timeBetweenShots; // Reset the time until the next shot
            }

            ///// TRANSITIONS:

            //transition: when the attack has satisfied its duration resume pursuit.
            
            if (attackDuration <= 0)
            {
                return new StatePursue();
            }
            

            return null;
        }
    }
}