using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// This class controls enemy movement, and states/behaviour
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// Prefab for enemys lazer shot bullet
        /// </summary>
        public Bullet bulletPrefab;

        /// <summary>
        /// prefab for orbiting ball
        /// </summary>
        public Orbit orbitPrefab;

        #region StateStff
        /// <summary>
        /// the transform that the enemy will follow
        /// </summary>
        public Transform attackTarget;

        /// <summary>
        /// the distance threshold to start pursuing the player in meters
        /// </summary>
        public float pursueDistanceThreshold = 10f;

        /// <summary>
        /// the distance threshold to start attacking the player in meters
        /// </summary>
        public float attackDistanceThreshold = 3f;

        /// <summary>
        /// the constantly rotating angle of the spin attack
        /// </summary>
        public float spinAngle;

        /// <summary>
        /// the current behavior state of the enemy
        /// </summary>
        EnemyState currentState;
        #endregion

        #region PhysicsStuff

        /// <summary>
        /// Velocity of the enemy in meters per second
        /// </summary>
        public Vector3 velocity = Vector3.zero;

        /// <summary>
        /// Deceleration rate of the enemy in meters per second
        /// </summary>
        public float deceleration = 4f;

        /// <summary>
        /// Acceleration rate of the enemy in meteres per second
        /// </summary>
        public float acceleration = 3f;

        /// <summary>
        /// Reference to the green healing particle system
        /// </summary>
        public ParticleSystem healer;

        #endregion

        /// <summary>
        /// Called once on start of game
        /// </summary>
        void Start()
        {
            //sets initial state to idle
            ChangeState(new StateIdle());
        }

        /// <summary>
        /// Called once per frame, and is therefore frame rate dependent
        /// </summary>
        void Update()
        {
            //have the spin attack spin faster whent the enemys health is lower
            if(gameObject.GetComponent<BossHealth>().health < 35)
            {
                spinAngle += Time.deltaTime * 150;
            }
            else
            {
            spinAngle += Time.deltaTime * 100;
            }

            if (spinAngle > 360) spinAngle = 0;

            EnemyState newState = currentState.Update();

            
            ChangeState(newState);

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            transform.position += velocity * Time.deltaTime;
            
        }

        /// <summary>
        /// Called every frame by Update
        /// Updates the current state if there was a change
        /// </summary>
        /// <param name="newState"></param>
        private void ChangeState(EnemyState newState)
        {

            if (newState != null)
            {
                if (currentState!=null)
                    currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }
        /// <summary>
        /// Spawns a projectile, and shoots it at the attack target
        /// </summary>
        public void ShootLazer()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);
            Instantiate(bulletPrefab, transform.position, rot);
        }

        /// <summary>
        /// Spawns an orbiting bullet sideways 
        /// </summary>
        public void ShootOrbit()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, dirToTarget);
            Instantiate(orbitPrefab, transform.position, rot);
        }

        /// <summary>
        /// Spawns projectiles out at the current spinAngle
        /// </summary>
        public void ShootSpin()
        {
          //  Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;
            Quaternion rot = Quaternion.Euler(0, spinAngle, 0);
          //  Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);
            Instantiate(bulletPrefab, transform.position, rot);
        }
    }
}