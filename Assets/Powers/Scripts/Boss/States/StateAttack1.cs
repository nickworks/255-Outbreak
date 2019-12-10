using UnityEngine;

namespace Powers
{
    public class StateAttack1 : EnemyState
    {
        //these are used for correct timing on a secondary sound effect
        private float timeStompSFX = 0.66f;
        private bool playedStompSFX = false;

        public override void OnBegin(BossController boss)
        {
            base.OnBegin(boss);

            //alert console to attacking and start the attack animation
            //Debug.Log("I'm stomping...");
            boss.agent.speed = 0f;

            //play animation and sfx
            boss.animator.Play("powers_anim_bossAttack1");
            boss.audioSource.PlayOneShot(boss.attackPrep1, 0.6f);
        }

        public override EnemyState Update()
        {
            //////////////////  state behaviour:

            //wait for appropriate timing to play stomp sound effect:
            if (timeStompSFX != 0)
            {
                timeStompSFX -= 1 * Time.deltaTime;
                timeStompSFX = Mathf.Clamp(timeStompSFX, 0, 0.66f);
            }
            else if (timeStompSFX == 0 && playedStompSFX == false)
            {
                playedStompSFX = true;
                boss.audioSource.PlayOneShot(boss.attackStomp1);
            }

            //once animation finishes, return to pursuing.
            if (!boss.animator.GetCurrentAnimatorStateInfo(0).IsName("powers_anim_bossAttack1")) return new StatePursue();

            return null;
        }
    }
}