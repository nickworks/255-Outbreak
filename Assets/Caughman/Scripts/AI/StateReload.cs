using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman {
    public class StateReload : EnemyState
    {
        /// <summary>
        /// How long in seconds it takes to reload
        /// </summary>
        float howLongReloadingTakes = 2;
        /// <summary>
        /// Time remaining until reload is finished
        /// </summary>
        float timeLeftUntilReloaded = 0;

        public override void onBegin(EnemyController enemy)
        {
            base.onBegin(enemy);

            timeLeftUntilReloaded = howLongReloadingTakes;
        }

        public override EnemyState Update()
        {
            ////////BEHAVIOR:

           // Debug.Log("I'm Reloading");


            timeLeftUntilReloaded -= Time.deltaTime;
            //////// TRANSITIONS TO OTHER STATES:
            //Return to State Pursue when finished reloading
            if(timeLeftUntilReloaded<= 0)
            {
                return new StatePursue();
            }
            //Transition to StateDeathsDoor if under 1000 hp
            if (enemy.bossBeserk == true)
            {
               // return new StateDeathsDoor();
            }

            return null; 
        }//End Update
    }
}
