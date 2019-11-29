using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuEStateReload : EnemyState
    {
        
        public float ReloadTimeLeft = 0;

        public override void OnBegin(BreuEnemyController enemy)
        {
            base.OnBegin(enemy);

            ReloadTimeLeft = Enemy.ReloadTime;
        }
        // Update is called once per frame
        public override EnemyState Update()
        {

            //////// Behavior

            Debug.Log("Reloading");




            //////// Transitions
            if (ReloadTimeLeft <= 0)
            {
                return new BreuEStatePursue();
            }



            return null;
        }
    }
}