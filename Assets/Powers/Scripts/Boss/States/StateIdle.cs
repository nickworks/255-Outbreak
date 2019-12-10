using UnityEngine;

namespace Powers
{
    public class StateIdle : EnemyState
    {
        public override void OnBegin(BossController boss)
        {
            base.OnBegin(boss);
        }

        public override EnemyState Update() {

            if (boss == null) return null; // there is no enemy to control
            if (boss.attackTarget == null) return null; // enemy has nothing it wants to attack...

            /////// state behaviour

            //Debug.Log("I'm idling...");

            /////// transition

            Vector3 disToTarget = boss.transform.position - boss.attackTarget.position;
            if(disToTarget.sqrMagnitude < boss.pursueDistanceThreshold * boss.pursueDistanceThreshold) {
                return new StatePursue();
            }


            return null;
        }
    }
}