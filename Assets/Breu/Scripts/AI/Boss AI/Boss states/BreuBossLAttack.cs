using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossLAttack : BreuBossState
    {
        
        public override BreuBossState Update()
        {
            //Debug.Log("Left Attacking");//for testing, comment out
            Movement();

            //get attack type from BossController
            BossPunch BP = Boss.HandLeft.GetComponent<BossPunch>();
            //if attack type is not null, do attack logic
            if (BP != null)
            {
                //if attack is finished, reset FinishedPunch and go to reset state
                if (BP.FinishedPunch == true)
                {
                    BP.FinishedPunch = false;

                    //Transition from Left Attack to Reset
                    return new BreuBossReset();
                }
                //if attack not finished, continue attack
                else
                {
                    BP.Punch();
                }
            }
            //if attacktype is null, throw error
            else
            {
                Debug.Log("Component missing on left hand!");
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

            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }
    }
}