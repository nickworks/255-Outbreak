using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossRAttack : BreuBossState
    {
        public override BreuBossState Update()
        {
            //Debug.Log("Right Attacking");//for testing, comment out

            Movement();

            //sets attack type from BossController
            BreuBossShoot BS = Boss.HandRight.GetComponent<BreuBossShoot>();
            //if attack type is not null, do attack logic
            if (BS != null)
            {
                //if attack is finished, reset FinishedAttack and go to Reset State
                if (BS.FinishedAttack == true)
                {
                    BS.FinishedAttack = false;
                    //Transition from Right Attack to Reset
                    return new BreuBossReset();
                }

                BS.Attack();

            }
            //if attack type is null, throw error
            else
            {
                Debug.Log("Component Missing on right hand!");
            }

            return null;

        }

        /// <summary>
        /// Sets velovity for each part and applies it
        /// </summary>
        void Movement()
        {
            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3((Boss.LeftStartPoint.position - Boss.HandLeft.position).x * Boss.AccelerationLeft * Time.deltaTime, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);

            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }
    }
}