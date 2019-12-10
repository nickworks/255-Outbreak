using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// Handles movement, projectile instantiation, and state transition
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// Prefab for the enemy's projectile
        /// </summary>
        public BulletController bulletPrefab;

        /// <summary>
        /// A reference for the barrel location
        /// </summary>
        public Transform projectileSpawnPoint;

        /// <summary>
        /// A reference to the location of the player
        /// </summary>
        public Transform attackTarget;

        /// <summary>
        /// Flag for the boss state logic
        /// </summary>
        public bool isBoss = false;

        /// <summary>
        /// The maximum amount of distance the enemy will begin to chase the player from
        /// </summary>
        public float pursueDistanceThreshold = 10;

        /// <summary>
        /// The maximum amount of distance the enemy will fire upon the player from
        /// </summary>
        public float attackDistanceThreshold = 3;

        /// <summary>
        /// Reference for the current state of this enemy
        /// </summary>
        EnemyState currentState;

        /// <summary>
        /// The velocity of this enemy in m/s
        /// </summary>
        public Vector3 velocity = Vector3.zero;

        /// <summary>
        /// The rate at which this enemy decelerates in m/s
        /// </summary>
        public float deceleration = 5;

        /// <summary>
        /// The rate at which this enemy accelerates in m/s
        /// </summary>
        public float acceleration = 3;

        /// <summary>
        /// Enters the state machine
        /// </summary>
        void Start()
        { 
            ChangeState(new StateIdle());
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {
            EnemyState newState = currentState.Update(); //Update the states - state - and receive a new state if transition is required
            ChangeState(newState); //Check to see if a transition is required

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration); //Slow down velocity due to deceleration
            transform.position += velocity * Time.deltaTime; // apply remaining velocity to position
        }
        
        /// <summary>
        /// Called every frame.
        /// Calls start and end functions on the affected states.
        /// </summary>
        /// <param name="newState"></param>
        private void ChangeState(EnemyState newState)
        {
            if (newState != null)
            {
                if (currentState != null)
                {
                    currentState.OnEnd(); // We have a new state, end the current one
                }
                currentState = newState;
                currentState.OnBegin(this); // Enter the new state
            }
        }

        /// <summary>
        /// Spawns a projectile and shoots it at the attack target
        /// </summary>
        public void ShootProjectile()
        {
            if (bulletPrefab != null)
            {
                Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

                Quaternion rotation = Quaternion.FromToRotation(Vector3.right, dirToTarget); // Using Vector3.right because the bullets are instantiated from Vector3.right
                Instantiate(bulletPrefab, projectileSpawnPoint.position, rotation);
            }            
        }

        /// <summary>
        /// Spawns a projectile from wherever the barrel is currently aimed
        /// </summary>
        public void ShootProjectileUnaimed()
        {
            Instantiate(bulletPrefab, projectileSpawnPoint.position, transform.rotation);
        }

        /// <summary>
        /// Spawns a projectile without aiming and rotates around the axis
        /// </summary>
        /// <param name="degreesToRotate"></param>
        public void ShootProjectileUnaimed(float degreesToRotate)
        {
            ShootProjectileUnaimed();
            Rotate(degreesToRotate);
        }

        /// <summary>
        /// Rotates around the y-axis
        /// </summary>
        /// <param name="degreesToRotate"></param>
        public void Rotate(float degreesToRotate)
        {
            transform.eulerAngles += new Vector3(0, degreesToRotate, 0);
        }

        /// <summary>
        /// Rotates toward the player
        /// </summary>
        public void RotateTowardPlayer()
        {
            transform.LookAt(attackTarget);
        }

        void Die()
        {

        }
    }
}