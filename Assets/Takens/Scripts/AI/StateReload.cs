using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// State for reloading
    /// acts as a cooldown between attacks
    /// </summary>
    public class StateReload : EnemyState
    {
        /// <summary>
        /// ammount of time it takes to cooldown in seconds
        /// </summary>
        float timeToReload = 2f;

        /// <summary>
        /// How much time is left in the cooldown in seconds
        /// </summary>
        float timeLeft;

        /// <summary>
        /// Called when enemy switches to reloading state
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            timeLeft = timeToReload;
        }

        /// <summary>
        /// Overriden update method that is called every frame
        /// Handles behavior and state transitions
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {


            //////// BEHAVIOR

            timeLeft -= Time.deltaTime;

            ///////// TRANSITIONS TO OTHER STATES

            //transition: switch to PURSUE when the cooldown is done
            if (timeLeft <= 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}