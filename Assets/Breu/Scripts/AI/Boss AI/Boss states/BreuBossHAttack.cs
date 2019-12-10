using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossHAttack : BreuBossState
    {
        public override BreuBossState Update()
        {
            //Debug.Log("Head Attacking");//for testing, comment out
            Movement();
            /*
            if (Boss.HeadAttackWarningRenderer != null)
            {
                Boss.HeadAttackWarningRenderer.material.color = Boss.ChargeColor;
            }
            */
            //Get the attack type from bosscontroller
            BreuHeadAttack HA = Boss.Head.GetComponent<BreuHeadAttack>();

            //checks if attack is null, if not fires attack then goes to reset state
            if (HA != null)
            {
                if (HA.FinishedAttack == true)
                {
                    HA.FinishedAttack = false;
                    /*
                    if (Boss.HeadAttackWarningRenderer != null)
                    {
                        Boss.HeadAttackWarningRenderer.material.color = Color.yellow;
                        Boss.HeadAttackWarningRenderer.enabled = false;
                    }
                    */
                    //Transition from Head Attack to Reset
                    return new BreuBossReset();
                }
                HA.Attack();
            }
            //if attack is null throw error
            else
            {
                Debug.Log("Component Missing on head!");
            }

            return null;
        }
        /// <summary>
        /// Sets velovity for each part and applies it
        /// </summary>
        void Movement()
        {
            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3((Boss.LeftStartPoint.position - Boss.HandLeft.position).x * Boss.AccelerationLeft * Time.deltaTime, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);
            
        }
    }
}