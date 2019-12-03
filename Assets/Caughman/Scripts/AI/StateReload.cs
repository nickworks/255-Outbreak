using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman {
    public class StateReload : EnemyState
    {
        float howLongReloadingTakes = 5;
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

            if(timeLeftUntilReloaded<= 0)
            {
                return new StatePursue();
            }


            return null; 
        }//End Update
    }
}
