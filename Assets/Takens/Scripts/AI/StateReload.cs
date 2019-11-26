using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    public class StateReload : EnemyState
    {
        float timeToReload = 2f;
        float timeLeft;


        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);
            timeLeft = timeToReload;
        }

        public override EnemyState Update()
        {


            //////// BEHAVIOR
            Debug.Log("Reloading...");

            timeLeft -= Time.deltaTime;

            ///////// TRANSITIONS TO OTHER STATES

            if (timeLeft <= 0)
            {
                return new StatePursue();
            }

            return null;
        }
    }
}