using UnityEngine;
using UnityEngine.AI;

namespace Powers
{
    public class BossController : MonoBehaviour
    {
        [HideInInspector]
        public NavMeshAgent agent;
        [HideInInspector]
        public Animator animator;
        public GameObject attackTwoEffect;
        public GameObject attackTwoTrigger;

        #region State Stuff
        public Transform attackTarget;
        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;
        EnemyState currentState;
        #endregion

        [Space(10)]

        //these are used to play sound effects
        public AudioSource audioSource;
        public AudioClip attackPrep1;
        public AudioClip attackStomp1;
        public AudioClip attackPrep2;
        public AudioClip attackPrep3;
        public AudioClip lazerSFX;

        [Space(10)]

        //this is used exclusively for attack two to see if the player was hit
        public HealthController player;

        void OnEnable()
        {
            //get nav mesh agent and animator
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            attackTwoEffect.SetActive(false);
            attackTwoTrigger.SetActive(false);

            ChangeState(new StateIdle());
        }

        void Update()
        {
            //if game is not paused, allow boss to operate
            if (!Game.isPaused)
            {
                EnemyState newState = currentState.Update();
                ChangeState(newState);
            }
        }

        private void ChangeState(EnemyState newState) {
            if (newState != null) {
                if(currentState != null) currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }

        private void OnDisable()
        {
            attackTwoEffect.SetActive(false);
            attackTwoTrigger.SetActive(false);
        }
    }
}