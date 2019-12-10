using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public abstract class BreuBossState
    {

        protected BreuBossController Boss;

        /// <summary>
        /// Used when state starts
        /// </summary>
        /// <param name="boss"></param>
        public virtual void OnBegin(BreuBossController boss)
        {
            this.Boss = boss;
        }

        /// <summary>
        /// Used when state ends
        /// </summary>
        public virtual void OnEnd()
        {

        }

        public abstract BreuBossState Update();
    }
}