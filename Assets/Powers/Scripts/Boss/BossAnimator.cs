using UnityEngine;
using UnityEngine.AI;

namespace Powers
{
    public class BossAnimator : MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent agent;
        private HealthController health;

        private void Start()
        {
            //detect necessary components
            animator = gameObject.GetComponent<Animator>();
            agent = gameObject.GetComponent<NavMeshAgent>();
            health = gameObject.GetComponent<HealthController>();
        }

        // Update is called once per frame
        void Update()
        {
            //if game is not paused, perform normally. otherwise, pause the animator
            if (!Game.isPaused)
            {
                animator.speed = 1;
                animator.SetFloat("speed", agent.velocity.magnitude);

                //play the death animation
                if (health.health == 0) animator.Play("powers_anim_bossDead");
            }
            else animator.speed = 0;
        }
    }
}

