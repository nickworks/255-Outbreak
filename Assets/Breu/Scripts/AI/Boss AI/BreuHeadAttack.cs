using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuHeadAttack : MonoBehaviour
    {
        public GameObject HeadAttack;//attack type

        public Transform AttackSpawn;//attack spawn location

        public float AttackTimer = 2;//how long a single attack lasts

        private float MaxAttackTime;//max of AttackTimer

        private int attacks = 0;//how many times has the head attacked

        public bool FinishedAttack = false;//if attack phase is complete

        void Start()
        {
            MaxAttackTime = AttackTimer;
        }

        public void Attack()
        {
            //if attacks are zero, attack
            if (attacks == 0)
            {
                Instantiate(HeadAttack, AttackSpawn.position, HeadAttack.transform.rotation);
                attacks++;
            }
            //if attack timer is great than zero, reduce attack timer
            if (AttackTimer > 0)
            {
                //Debug.Log("Head attack counting down");
                AttackTimer -= Time.deltaTime;
            }
            //if attack timer is not greater than zero, reset attack timer and attacks then switch FinishedAttack to true
            else
            {
                //Debug.Log("Head attack ended");
                AttackTimer = MaxAttackTime;
                attacks = 0;
                FinishedAttack = true;
            }

        }

    }
}