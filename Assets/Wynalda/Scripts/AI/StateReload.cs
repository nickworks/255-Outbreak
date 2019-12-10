using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class StateReload : EnemyState
    {
        float howLongReloadingTakes = 2;
        float timeLeftUntilReloaded = 0;

        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            timeLeftUntilReloaded = howLongReloadingTakes;
        }


        public override EnemyState Update()
        {
            ////////// BEHAVIOUR

            //Debug.Log("Im reloading...");

            timeLeftUntilReloaded -= Time.deltaTime;

            ////////// TRANSITION

            if(timeLeftUntilReloaded <= 0)
            {
                return new StateIdle();
            }

            return null;
        }
    }
}