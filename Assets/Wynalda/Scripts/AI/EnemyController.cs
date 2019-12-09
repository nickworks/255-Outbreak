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
    
        public void ShootCircleProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;
            //Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget); //Not used anymore, was used for aiming a single shot at the player.
            float yaw = transform.eulerAngles.y;
            float spread = 5;
            float spread2 = 15;
            float spread3 = 25;
            float spread4 = 35;
            float spread5 = 45;
            float spread6 = 55;
            float spread7 = 65;
            float spread8 = 75;
            float spread9 = 85;
            float spread10 = 95;
            float spread11 = 105;
            float spread12 = 115;
            float spread13 = 125;
            float spread14 = 135;
            float spread15 = 145;
            float spread16 = 155;
            float spread17 = 165;
            float spread18 = 175;
            float spread19 = 185;
            float spread20 = 195;
            float spread21 = 205;
            float spread22 = 215;
            float spread23 = 225;

            Bullet bill2 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw-spread, 0));
            Bullet bill3 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw+spread, 0));
            Bullet bill11 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw-spread2, 0));
            Bullet bill12 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw+spread2, 0));
            Bullet bill13 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw-spread3, 0));
            Bullet bill14 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw+spread3, 0));
            Bullet bill10 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw-spread4, 0));
            Bullet bill15 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw+spread4, 0));
            Bullet bill16 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread5, 0));
            Bullet bill17 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread5, 0));
            Bullet bill18 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread6, 0));
            Bullet bill19 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread6, 0));
            Bullet bill20 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread7, 0));
            Bullet bill21 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread7, 0));
            Bullet bill22 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread8, 0));
            Bullet bill23 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread8, 0));
            Bullet bill24 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread9, 0));
            Bullet bill25 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread9, 0));
            Bullet bill26 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread10, 0));
            Bullet bill27 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread10, 0));
            Bullet bill28 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread11, 0));
            Bullet bill29 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread11, 0));
            Bullet bill30 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread12, 0));
            Bullet bill31 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread12, 0));
            Bullet bill32 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread13, 0));
            Bullet bill33 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread13, 0));
            Bullet bill34 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread14, 0));
            Bullet bill35 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread14, 0));
            Bullet bill36 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread15, 0));
            Bullet bill37 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread15, 0));
            Bullet bill38 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread16, 0));
            Bullet bill39 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread16, 0));
            Bullet bill40 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread17, 0));
            Bullet bill41 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread17, 0));
            Bullet bill42 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread18, 0));
            Bullet bill43 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread18, 0));
            Bullet bill44 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread19, 0));
            Bullet bill45 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread19, 0));
            Bullet bill46 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread20, 0));
            Bullet bill47 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread20, 0));
            Bullet bill48 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread21, 0));
            Bullet bill49 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread21, 0));
            Bullet bill50 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread22, 0));
            Bullet bill51 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread22, 0));
            Bullet bill52 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw - spread23, 0));
            Bullet bill53 = Instantiate(triplePrefab, transform.position, Quaternion.Euler(0, yaw + spread23, 0));

            bill2.bulletShooter = transform;
            bill3.bulletShooter = transform;
            bill10.bulletShooter = transform;
            bill11.bulletShooter = transform;
            bill12.bulletShooter = transform;
            bill13.bulletShooter = transform;
            bill14.bulletShooter = transform;
            bill15.bulletShooter = transform;
            bill16.bulletShooter = transform;
            bill17.bulletShooter = transform;
            bill18.bulletShooter = transform;
            bill19.bulletShooter = transform;
            bill20.bulletShooter = transform;
            bill21.bulletShooter = transform;
            bill22.bulletShooter = transform;
            bill23.bulletShooter = transform;
            bill24.bulletShooter = transform;
            bill25.bulletShooter = transform;
            bill26.bulletShooter = transform;
            bill27.bulletShooter = transform;
            bill28.bulletShooter = transform;
            bill29.bulletShooter = transform;
            bill30.bulletShooter = transform;
            bill31.bulletShooter = transform;
            bill32.bulletShooter = transform;
            bill33.bulletShooter = transform;
            bill34.bulletShooter = transform;
            bill35.bulletShooter = transform;
            bill36.bulletShooter = transform;
            bill37.bulletShooter = transform;
            bill38.bulletShooter = transform;
            bill39.bulletShooter = transform;
            bill40.bulletShooter = transform;
            bill41.bulletShooter = transform;
            bill42.bulletShooter = transform;
            bill43.bulletShooter = transform;
            bill44.bulletShooter = transform;
            bill45.bulletShooter = transform;
            bill46.bulletShooter = transform;
            bill47.bulletShooter = transform;
            bill48.bulletShooter = transform;
            bill49.bulletShooter = transform;
            bill50.bulletShooter = transform;
            bill51.bulletShooter = transform;
            bill52.bulletShooter = transform;
            bill53.bulletShooter = transform;
        }
      

        public void ShootStrongProjectile()
        {
            Vector3 dirToTarget = (attackTarget.position - transform.position).normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.right, dirToTarget);

            Bullet bill4 = Instantiate(autoPrefab, transform.position, rot);
            Bullet bill5 = Instantiate(autoPrefab, transform.position, rot);
            Bullet bill6 = Instantiate(autoPrefab, transform.position, rot);
            Bullet bill7 = Instantiate(autoPrefab, transform.position, rot);
            Bullet bill8 = Instantiate(autoPrefab, transform.position, rot);
            Bullet bill9 = Instantiate(autoPrefab, transform.position, rot);
            bill4.bulletShooter = transform;
            bill5.bulletShooter = transform;
            bill6.bulletShooter = transform;
            bill7.bulletShooter = transform;
            bill8.bulletShooter = transform;
            bill9.bulletShooter = transform;
        }


    }
}