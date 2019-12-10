using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossShoot : MonoBehaviour
    {
        public GameObject RightAttack;

        public Transform AttackSpawn;

        public float BetweenShotTime;
        private float ReloadTimer;

        private int Attacks = 0;
        public bool FinishedAttack = false;

        
        public void Attack()
        {
            //if attack less than three, do attack logic
            if (Attacks < 3)
            {
                //if reload timer is less than or equal to zero, fire
                if (ReloadTimer <= 0)
                {
                    Instantiate(RightAttack, AttackSpawn.position, transform.rotation);
                    ReloadTimer = BetweenShotTime;
                    Attacks++;
                    //Debug.Log("Fired right");
                }
                //if reload timer is greater than zero, reduce reload timer
                else
                {
                    ReloadTimer -= Time.deltaTime;
                    //Debug.Log("Reduced time between right fires");
                }
            }
            //if attacks are equal to or more than three, reset attack to zero and set FinishedAttack to true
            else
            {
                //Debug.Log("Remaining attacks reset");
                Attacks = 0;
                FinishedAttack = true;
            }
        }
    }
}