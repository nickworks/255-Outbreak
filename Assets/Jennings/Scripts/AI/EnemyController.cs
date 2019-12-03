using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {
    public class EnemyController : MonoBehaviour {

        public Bullet bulletPrefab;

        // State stuff:
        #region State Stuff
        public Transform attackTarget;

        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;
        EnemyState currentState;

        #endregion
        // Physics stuff:
        #region Physics Stuff
        public Vector3 velocity = Vector3.zero;
        public float deceleration = 10;
        public float acceleration = 3;
        #endregion
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
                if(currentState != null) currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }

        // Spawns a projectile & shoots it at the attack target
        public void ShootProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Instantiate(bulletPrefab, transform.position, rot);
        }
    }
}
