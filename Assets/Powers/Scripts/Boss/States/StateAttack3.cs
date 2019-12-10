using UnityEngine;

namespace Powers
{
    public class StateAttack3 : EnemyState
    {
        //these are used for correct timing on a secondary sound effect
        private float lazerSFXTiming = 1;
        private bool playedLazerSFX = false;


        public override void OnBegin(BossController boss)
        {
            base.OnBegin(boss);

            //alert console to attacking and start the attack animation
            //Debug.Log("I'm a firing my lazer...");

            boss.agent.speed = 0f;
            boss.audioSource.PlayOneShot(boss.attackPrep3);
            boss.animator.Play("powers_anim_bossAttack3");
        }

        public override EnemyState Update()
        {
            //////////////////  state behaviour:

            if (lazerSFXTiming != 0)
            {
                lazerSFXTiming -= 1 * Time.deltaTime;
                lazerSFXTiming = Mathf.Clamp(lazerSFXTiming, 0, 1);
            }
            else if (playedLazerSFX == false)
            {
                boss.audioSource.PlayOneShot(boss.lazerSFX);
                playedLazerSFX = true;
            }

            //once animation finishes, return to pursuing.
            if (!boss.animator.GetCurrentAnimatorStateInfo(0).IsName("powers_anim_bossAttack3")) return new StatePursue();

            return null;
        }
    }
}