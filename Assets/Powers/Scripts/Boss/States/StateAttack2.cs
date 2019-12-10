using UnityEngine;

namespace Powers
{
    public class StateAttack2 : EnemyState
    {
        private float attackTime;

        public override void OnBegin(BossController boss)
        {
            base.OnBegin(boss);

            //alert console to attacking and start the attack animation
            //.Log("I'm charging...");

            //play prep animation and sfx
            boss.animator.Play("powers_anim_bossAttack2Prepare");
            boss.audioSource.PlayOneShot(boss.attackPrep2, 0.6f);

            attackTime = 5; //this makes the attack last 5 seconds
            boss.agent.speed = 0f;
            boss.agent.stoppingDistance = 0.1f; //change stopping distance so boss runs into player
        }

        public override EnemyState Update()
        {
            //////////////////  state behaviour:

            //do a preparation animation. once it completes, the boss while charge
            if (!boss.animator.GetCurrentAnimatorStateInfo(0).IsName("powers_anim_bossAttack2Prepare"))
            {
                //enable the attack effect and the attack trigger.
                boss.attackTwoEffect.SetActive(true);
                boss.attackTwoTrigger.SetActive(true);

                //set the agent to charge towards the player
                boss.agent.speed = 4.5f;
                boss.agent.destination = boss.attackTarget.transform.position;

                //update attack time. if the player has been hit yet or the attack has ended, return to pursuing
                if (attackTime != 0)
                {
                    attackTime -= 1 * Time.deltaTime;
                    attackTime = Mathf.Clamp(attackTime, 0, 5);
                }

                if (boss.player.gotHit && !boss.player.gotHitLast || attackTime == 0)
                {
                    boss.agent.speed = 4f;
                    boss.attackTwoTrigger.SetActive(false);
                    return new StatePursue();
                }
            }

            return null;
        }
    }
}