using UnityEngine;

namespace Powers
{
    public abstract class EnemyState
    {
        protected BossController boss;

        /// This method is called when switching to this state.
        /// <param name="boss"></param>
        public virtual void OnBegin(BossController boss) {
            this.boss = boss;
        }

        // This method is called when switching away from this state.
        public virtual void OnEnd() {

        }

        public abstract EnemyState Update();
    }
}