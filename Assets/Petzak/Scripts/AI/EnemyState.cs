using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Base class for enemy state
    /// </summary>
    public abstract class EnemyState
    {
        /// <summary>
        /// Controller for the enemy
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
        public virtual void OnEnd()
        {

        }

        /// <summary>
        /// Used by children classes to switch states
        /// </summary>
        /// <returns></returns>
        public abstract EnemyState Update();
    }
}