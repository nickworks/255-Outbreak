using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breu
{
    public abstract class EnemyState
    {
        protected BreuEnemyController Enemy;


        /// <summary>
        /// used when state starts
        /// </summary>
        /// <param name="enemy"></param>
        public virtual void OnBegin(BreuEnemyController enemy)
        {
            this.Enemy = enemy;
        }


        /// <summary>
        /// used when state ends
        /// </summary>
        public virtual void onEnd()
        {

        }

        public abstract EnemyState Update();

    }
}