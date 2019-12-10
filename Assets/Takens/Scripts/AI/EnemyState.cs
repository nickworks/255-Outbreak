using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Abstract class that the enemy states inherit from
    /// </summary>
    public abstract class EnemyState
    {
        /// <summary>
        /// reference to the enemyController component
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
        /// this method is called when switching away from this state
        /// </summary>
        public virtual void OnEnd()
        {

        }
        /// <summary>
        /// This method is called once per frame
        /// </summary>
        /// <returns></returns>
        public abstract EnemyState Update();
        
    }
}