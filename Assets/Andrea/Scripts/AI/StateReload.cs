using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for enemy reload
    /// </summary>
    public class StateReload : EnemyState
    {
        float reloadTime = 2.5f; 
        float reloadTimeRemaining = 0; 


        /// <summary>
        /// Called upon entering the state, sets the reload time
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            reloadTimeRemaining = reloadTime;
        }

        /// <summary>
        /// Called each frame, decrements the reload timer
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            //Debug.Log("I'm reloading...");
            reloadTimeRemaining -= Time.deltaTime;

            if (reloadTimeRemaining < 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}