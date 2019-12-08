using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {

    public class StateReload : EnemyState {

        float howLongRealodingTakes = 5;
        float timeLeftUntilReloaded = 0;

        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            timeLeftUntilReloaded = howLongRealodingTakes;
        }


        public override EnemyState Update()
        {

            // BEHAVIOR:

            Debug.Log("I'm reloading...");

            timeLeftUntilReloaded -= Time.deltaTime;


            // TRANSITIONS TO OTHER STATES:

            if(timeLeftUntilReloaded <= 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}
