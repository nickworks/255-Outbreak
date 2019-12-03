using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class EnemyController : MonoBehaviour
    {

        public Bullet bulletPrefab;

        public Transform attackTarget;
        EnemyState currentState;
        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;
        
        public Vector3 velocity = Vector3.zero;
        public float deceleration = 10;
        public float acceleration = 3;
        

        void Start()
        {
            ChangeState(new StateIdle());
        }

        
        void Update()
        {
           EnemyState newState = currentState.Update();
           ChangeState(newState);

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            transform.position += velocity * Time.deltaTime;
        }

        private void ChangeState(EnemyState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }
        /// <summary>
        /// Spawns a projectile and shoots it at the attack target.
        /// </summary>
        public void ShootProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Instantiate(bulletPrefab, transform.position, rot);
        }

        void Die()
        {
            print("aaaaaaahhhhhh");
        }


    }
}
