using UnityEngine;

namespace Powers
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator animator;
        private PlayerMovement playerMovement;
        private HealthController health;

        private void Start()
        {
            //detect necessary components
            animator = gameObject.GetComponent<Animator>();
            playerMovement = gameObject.GetComponent<PlayerMovement>();
            health = gameObject.GetComponent<HealthController>();
        }

        // Update is called once per frame
        void Update()
        {
            //if game is not paused, perform normally. otherwise, pause the animator
            if (!Game.isPaused)
            {
                animator.speed = 1;
                animator.SetFloat("speed", playerMovement.currentSpeed);

                //play the death animation
                if (health.health == 0) animator.Play("powers_anim_playerDead");
            }
            else animator.speed = 0;
        }
    }
}

