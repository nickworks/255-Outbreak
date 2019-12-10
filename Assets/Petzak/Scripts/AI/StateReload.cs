using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// State for enemy reloading
    /// NOT IMPLEMENTED
    /// </summary>
    public class StateReload : EnemyState
    {
        float howLongReloadingTakes = 5;
        float timeLeftUntilReloaded = 0;

        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            timeLeftUntilReloaded = howLongReloadingTakes;
        }

        public override EnemyState Update()
        {
            ///////////// BEHAVIOR:

            timeLeftUntilReloaded -= Time.deltaTime;

            ///////////// TRANSITIONS TO OTHER STATES:

            if (timeLeftUntilReloaded <= 0)
                return new StatePursue();

            return null;
        }
    }
}