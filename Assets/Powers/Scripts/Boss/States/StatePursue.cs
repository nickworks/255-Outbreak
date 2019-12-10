using UnityEngine;

namespace Powers
{
    public class StatePursue : EnemyState
    {
        private float waitToAttack = 2f; //this keeps the boss from repeatedly attacking

        public override void OnBegin(BossController boss)
        {
            base.OnBegin(boss);

            //alert console of pursuit
            //Debug.Log("I'm pursuing");
        }

        public override EnemyState Update() {
            //////////////  state behaviour:

            // move towards the player
            boss.agent.speed = 4f;
            boss.agent.stoppingDistance = 1f;
            boss.agent.destination = boss.attackTarget.transform.position;

            ////////////// transition:

            Vector3 disToTarget = boss.transform.position - boss.attackTarget.position;
            float disSqr = disToTarget.sqrMagnitude;

            // switch to IDLE if player is too far
            if(disSqr > boss.pursueDistanceThreshold * boss.pursueDistanceThreshold) {
                return new StateIdle();
            }

            // switch to ATTACK if the player is close
            if(disSqr < boss.attackDistanceThreshold * boss.attackDistanceThreshold && waitToAttack == 0) {

                //use a random int, and decide the attack depending on the int
                int attack = Random.Range(0,6);

                if(attack <= 2) return new StateAttack1();
                else if (attack == 3 || attack == 4) return new StateAttack2();
                else if (attack >= 5) return new StateAttack3();
            }

            if (waitToAttack != 0)
            {
                waitToAttack -= 2 * Time.deltaTime;
                waitToAttack = Mathf.Clamp(waitToAttack, 0, 2);
            }

            return null;
        }
    }
}