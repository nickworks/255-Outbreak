using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class EnemyController : MonoBehaviour
    {
        //Bullet Prefabs:
        public Bullet bossBulletOne;
        public Bullet bossBulletTwo;
        public Bullet bossBulletThree;

        //States stuff:
        public Transform attackTarget;
        public float pursueDistanceThreshold = 10;
        public float attackDistanceThreshold = 3;

        EnemyState currentState;


        //Physics Stuff:
        public Vector3 velocity = Vector3.zero;
        public float deceleration = 5;
        public float acceleration = 10;


        /// <summary>
        /// Whether the Boss Has Been Killed
        /// </summary>
        private bool bossDead = false;
        /// <summary>
        /// Delay Before Loading Next Level
        /// </summary>
        private float delayBeforeNextLevel = 10;
        /// <summary>
        /// is the Boss under 1000 hp?
        /// </summary>
        public bool bossBeserk = false;


        
        void Update()
        {
            EnemyState newState = currentState.Update();
            ChangeState(newState);

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            transform.position += velocity * Time.deltaTime;

            if(bossDead == true)
            {
                delayBeforeNextLevel--;
            }

            if (delayBeforeNextLevel<= 0)
            {
                NextLevel();
            }
            
        }//End Update

        /// <summary>
        /// Allows enemy to switch from state to state based on parameters
        /// </summary>
        /// <param name="newState"></param>
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
       public void ShootLongBullet()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);


           //float spread = 10;

            //float yaw = transform.eulerAngles.y;

            Bullet bill = Instantiate(bossBulletOne, transform.position, Quaternion.FromToRotation(Vector3.right, dirToTarget));

            bill.bulletShooter = transform;

        }
        /// <summary>
        /// Shoots slow large bullets that block player movement
        /// </summary>
        public void ShootBerserkBullet()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Bullet bill = Instantiate(bossBulletThree, transform.position, Quaternion.FromToRotation(Vector3.right, dirToTarget));
            Bullet bill2 = Instantiate(bossBulletThree, transform.position, Quaternion.FromToRotation(Vector3.left, dirToTarget));

            bill.bulletShooter = transform;
            bill2.bulletShooter = transform;

        }
        /// <summary>
        /// Shoots a fast wide bullet
        /// </summary>
        public void ShootWideBullet()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Bullet bill = Instantiate(bossBulletTwo, transform.position, Quaternion.FromToRotation(Vector3.right, dirToTarget));

            bill.bulletShooter = transform;

        }

        /// <summary>
        /// What to do when the Enemies health is under 1000 
        /// </summary>
        void Berserk()
        {
            print("On Deaths Door");
            bossBeserk = true;
        }

        /// <summary>
        /// What to do when the Enemy is Dead
        /// </summary>
        void Die()
        {
            print("Boss is dead");
            bossDead = true;
            Game.GotoNextLevel();

        }

        /// <summary>
        /// What to do when Bullet Collides with Enemy
        /// </summary>
        void Hit()
        {
           

        }

        /// <summary>
        /// Goes to Next Level after Boss is Dead
        /// </summary>
        void NextLevel()
        {
            Game.GotoNextLevel();
            print("Going to Next Level now");
        }
    }//End EnemyController
}
