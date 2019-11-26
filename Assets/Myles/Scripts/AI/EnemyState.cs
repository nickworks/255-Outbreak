using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public abstract class EnemyState 
    {
        protected EnemyController enemy;

        /// <summary>
        /// This method is called when switching to the state.
        /// </summary>
        /// <param name="enemy"></param>

        public virtual void OnBegin(EnemyController enemy)
        {
            this.enemy = enemy;
        }

        /// <summary>
        /// This method is called when switching away from the state.
        /// </summary>

        public virtual void OnEnd()
        {

        }

        public abstract EnemyState Update();
    }
}
