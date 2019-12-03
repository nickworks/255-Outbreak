using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    public class StateReload : EnemyState
    {
        float reloadTime = 5;
        float reloadTimeRemaining = 0;

        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            reloadTimeRemaining = reloadTime;
        }

        public override EnemyState Update()
        {
            Debug.Log("I'm reloading...");
            reloadTimeRemaining -= Time.deltaTime;

            if (reloadTimeRemaining <0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}