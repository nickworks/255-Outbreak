using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class EnemyController : MonoBehaviour
    {
        public Bullet peaPrefab;
        public Bullet autoPrefab;
        public Bullet triplePrefab;
        
        //state stuff
        #region State Stuff
        public Transform attackTarget;
        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;
        #endregion

        EnemyState currentState;

        #region Physics
        //physics stuff
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

        private void ChangeState(EnemyState newState) { 
            if(newState != null)
            {
                if(currentState != null)currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }


        /// <summary>
        /// Spawns a projectile, and shoots it at the attack target.
        /// </summary>
        public void ShootBasicProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Bullet bill = Instantiate(peaPrefab, transform.position, rot);
            bill.bulletShooter = transform;
        }
        public void ShootTripleProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            float yaw = transform.eulerAngles.y;
            float spread = 10;

            Bullet bill1 = Instantiate(triplePrefab, transform.position, rot);
            Bullet bill2 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw-spread, 0));
            Bullet bill3 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw+spread, 0));


        }

    }
}