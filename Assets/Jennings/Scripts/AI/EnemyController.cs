using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {
    public class EnemyController : MonoBehaviour {

        public Bullet bulletPrefab; // pulls up the bullet prefab

        // State stuff:
        #region State Stuff
        public Transform attackTarget; // looks for position of the target to be attacked
        public Transform enemyProjectileSpawnPoint; // determines position of the enemy's projectile spawn point

        public float pursueDistanceThreshold = 10; // determines the distance of enemy for pursuing
        public float attackDistanceThreshold = 3; // determines the distance of enemy for attacking
        EnemyState currentState; // determine's enemy's current state

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
        // The act of changing state for enemy AI
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

            // Old instantiation location of bullets, would cause for enemy to kill itself
            //Instantiate(bulletPrefab, transform.position, rot); 

            // New Instantiation location of bullets, causes for enemy to shoot without self harm
            Instantiate(bulletPrefab, enemyProjectileSpawnPoint.position, rot);

        }

        // What happens upon enemy death
        void Die()
        {
            // Code to test it going to the next level
            // Debug.Log("I am going to next level..."); 


            // Prints Noooo upon defeating the enemy and is supposed to go to the next level (not functioning correctly)
            print("Nooooo!!!");
            Game.GotoNextLevel();

        }

    }
}
