using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Controller for spider boss
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// Prefab of bullet to spawn
        /// </summary>
        public Bullet bulletPrefab;
        /// <summary>
        /// Spider animation
        /// </summary>
        public Animation animation;

        #region State Stuff
        /// <summary>
        /// Target that the enemy is attacking (player
        /// </summary>
        public Transform attackTarget;
        /// <summary>
        /// Distance to start pursuing
        /// </summary>
        public float pursueDistanceThreshold = 10;
        /// <summary>
        /// Distance to start attacking
        /// </summary>
        public float attackDistanceThreshold = 3;
        /// <summary>
        /// Time it takes for lunge animation
        /// </summary>
        public float lungeTime = 60;
        /// <summary>
        /// Current state that the enemy is in
        /// </summary>
        EnemyState currentState;
        #endregion

        #region Physics
        public Vector3 velocity = Vector3.zero;
        public float deceleration = 10;
        public float acceleration = 20;
        #endregion
        /// <summary>
        /// True when enemy is dying, in order to play death animation
        /// </summary>
        public bool dying = false;

        /// <summary>
        /// Called on start
        /// </summary>
        void Start()
        {
            ChangeState(new StatePursue());
            animation = GetComponent<Animation>();
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        void Update()
        {
            if (dying)
            {
                Animation a = GetComponent<Animation>();
                a.Play("death1");
                Game.GotoNextLevel();

                return;
            }

            if (currentState == null)
            {
                ChangeState(new StatePursue());
            }
            else
            {
                EnemyState newState = currentState.Update();
                ChangeState(newState);
            }

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            transform.position += velocity * Time.deltaTime;
        }

        /// <summary>
        /// Swaps to new enemy state
        /// </summary>
        /// <param name="newState"></param>
        private void ChangeState(EnemyState newState)
        {
            if (newState != null)
            {
                if (currentState != null)
                    currentState.OnEnd();
                currentState = newState;
                currentState.OnBegin(this);
            }
        }

        /// <summary>
        /// Spawns a projectile, and shoots it at the attack target.
        /// </summary>
        public void ShootProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Bullet bill = Instantiate(bulletPrefab, transform.position, rot);
            bill.bulletShooter = transform;
        }

        /// <summary>
        /// Shoots a randomized spread shot
        /// </summary>
        public void ShootSpread()
        {
            System.Random r = new System.Random();
            for (int i = 0; i < r.Next(8, 16); i++)
            {
                Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;
                Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);
                float yaw = rot.eulerAngles.y;
                Bullet bill = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, yaw + r.Next(-30, 30), 0));
                bill.bulletShooter = transform;
                bill.speed = 5;
            }
        }

        /// <summary>
        /// Triggered when enemy is dies, starts death animation
        /// </summary>
        void Die()
        {
            dying = true;
        }
    }
}