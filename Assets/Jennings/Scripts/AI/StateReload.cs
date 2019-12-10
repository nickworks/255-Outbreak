using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {

    public class StateReload : EnemyState {

        float howLongReloadingTakes = 5; // Checks to see how long reloading takes 
        float timeLeftUntilReloaded = 0; // Checks remaining time until reloaded

        // Checks to see how long reloading will take and reloads
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            timeLeftUntilReloaded = howLongReloadingTakes;
        }


        public override EnemyState Update()
        {

            // BEHAVIOR: Reloads/checks to see if reloading

            //Debug.Log("I'm reloading...");

            timeLeftUntilReloaded -= Time.deltaTime;


            // TRANSITIONS TO OTHER STATES:
            // Specifically pursue

            if(timeLeftUntilReloaded <= 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}
