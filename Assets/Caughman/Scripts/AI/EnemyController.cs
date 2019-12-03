using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class EnemyController : MonoBehaviour
    {
        public Bullet bulletPrefab;

        //States stuff:
        public Transform attackTarget;
        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;

        EnemyState currentState;


        //Physics Stuff:

        public Vector3 velocity = Vector3.zero;
        public float deceleration = 5;
        public float acceleration = 10;

        void Start()
        {
            ChangeState(new StateIdle());
        }//End Start

        
        void Update()
        {
            EnemyState newState = currentState.Update();
            ChangeState(newState);

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            transform.position += velocity * Time.deltaTime;
        }//End Update

        private void ChangeState(EnemyState newState)
        {
            if (newState != null)
            {
                if(currentState != null) currentState.onEnd();
                currentState = newState;
                currentState.onBegin(this);
            }
        }//End ChangeState

        /// <summary>
        /// Spawns a projectile and shoots it at the attack target
        /// </summary>
       public void ShootProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Instantiate(bulletPrefab, transform.position, rot);
        }
    }//End EnemyController
}
