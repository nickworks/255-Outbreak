using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// Abstract base class for the enemy state machine.
    /// </summary>
    public abstract class EnemyState
    {
        /// <summary>
        /// Reference for the EnemyController component.
        /// </summary>
        protected EnemyController enemy;

        /// <summary>
        /// This method is called when switching to this state.
        /// </summary>
        /// <param name="enemy"></param>
        public virtual void OnBegin(EnemyController enemy)
        {
            this.enemy = enemy;
        }

        /// <summary>
        /// This method is called when switching away from this state.
        /// </summary>
        public void OnEnd()
        {

        }

        public abstract EnemyState Update();
    }
}