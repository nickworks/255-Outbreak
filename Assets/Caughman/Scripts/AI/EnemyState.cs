using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public abstract class EnemyState 
    {
        /// <summary>
        /// Which Enemy the State is Running on
        /// </summary>
       protected EnemyController enemy;
        /// <summary>
        /// This method is called when switching to this state
        /// </summary>
        /// <param name="enemy"></param>
        public virtual void onBegin(EnemyController enemy)
        {
            this.enemy = enemy;
        }
        /// <summary>
        /// This method is called when switching away from this state
        /// </summary>
        public virtual void onEnd()
        {

        }

        public abstract EnemyState Update();

    }//End EnemyState
}
