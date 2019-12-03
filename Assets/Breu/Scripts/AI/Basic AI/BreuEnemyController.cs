using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuEnemyController : MonoBehaviour
    {
        public GameObject Weapon;
        public float WeaponCooldown = 2;
        public float ReloadTime = 5;

        [HideInInspector]
        public  float WeaponWait;
        
        #region state variable
        public Transform Target;
        public float PursueDistanceThreshold = 10;
        public float AttackDistanceThreshold = 3;

        EnemyState CurrentState;
        #endregion

        #region physics variable
        public float Deceleration = 2;
        public float Acceleration = 2;
        [HideInInspector]
        public Vector3 velocity = Vector3.zero;
        #endregion

        void Start()
        {
            ChangeState(new BreuEStateIdle());
            WeaponWait = WeaponCooldown;
        }
        
        void Update()
        {
            WeaponWait -= Time.deltaTime;

            //State
            EnemyState newState = CurrentState.Update();

            ChangeState(newState);

            // physiscs based movement:
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * Deceleration);
            transform.position += velocity * Time.deltaTime;
        }

        private void ChangeState(EnemyState newState)
        {
            if (newState != null)
            {
                if (CurrentState != null)
                {
                    CurrentState.onEnd();
                }
                CurrentState = newState;
                CurrentState.OnBegin(this);
            }
        }


        /// <summary>
        /// spawns attack aimed at target
        /// </summary>
        public void FireAttack()
        {
                Vector3 DirToTarget = (Target.position - transform.position).normalized;

                Quaternion rot = Quaternion.FromToRotation(Vector3.right, DirToTarget);

                Instantiate(Weapon, transform.position, rot);

                WeaponWait = WeaponCooldown;
        }
    }
}