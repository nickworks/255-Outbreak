using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breu
{
    public class BreuBossRCharge : BreuBossState
    {
        float CTimer;
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeRight;
        }

        public override BreuBossState Update()
        {
            Debug.Log("Right Charging");//for testing, comment out

            movement();

            Boss.ChargeTimeRight -= Time.deltaTime;



            //transition from Charging to Attack
            if (Boss.ChargeTimeRight <= 0)
            {
                Boss.ChargeTimeRight = CTimer;
                return new BreuBossRAttack();
            }


            return null;
        }

        private void movement()
        {
            Vector3 DirToTarget = (Boss.Target.position - Boss.HandRight.position).normalized;
            Boss.VelocityRight += new Vector3(0, 0, DirToTarget.z * Boss.AccelerationRight * Boss.AccelerationRight * Time.deltaTime);

            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3(0, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);

            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }
    }
}