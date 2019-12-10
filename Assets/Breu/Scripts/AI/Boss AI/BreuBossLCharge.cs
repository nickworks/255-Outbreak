using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossLCharge : BreuBossState
    {
        float CTimer;
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeLeft;
        }

        public override BreuBossState Update()
        {
            Debug.Log("Left Charging");//for testing, comment out

            Movement();

            Boss.ChargeTimeLeft -= Time.deltaTime;

            if (Boss.ChargeTimeLeft <= 0)
            {
                Boss.ChargeTimeLeft = CTimer;
                return new BreuBossLAttack();
            }


            return null;
        }

        private void Movement()
        {
            Vector3 DirToTarget = (Boss.Target.position - Boss.HandLeft.position).normalized;
            Boss.VelocityLeft += new Vector3(0, 0, DirToTarget.z * Boss.AccelerationLeft * Boss.AccelerationLeft * Time.deltaTime);

            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }
    }
}
