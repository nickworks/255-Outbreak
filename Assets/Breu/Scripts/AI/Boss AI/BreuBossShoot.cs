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
        private float ShotTimer;

        private int Attacks = 0;
        public bool FinishedAttack = false;

        
        public void Attack()
        {
            if (Attacks < 3)
            {
                //Debug.Log("Right attacks less than three");
                if (ShotTimer <= 0)
                {
                    Instantiate(RightAttack, AttackSpawn.position, transform.rotation);
                    ShotTimer = BetweenShotTime;
                    Attacks++;
                    //Debug.Log("Fired right");
                }
                else
                {
                    ShotTimer -= Time.deltaTime;
                    //Debug.Log("Reduced time between right fires");
                }
            }
            else
            {
                //Debug.Log("Remaining attacks reset");
                Attacks = 0;
                FinishedAttack = true;
            }
        }
    }
}