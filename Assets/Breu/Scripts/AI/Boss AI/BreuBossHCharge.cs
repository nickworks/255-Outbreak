using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossHCharge : BreuBossState
    {
        float CTimer;
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeHead;
        }

        public override BreuBossState Update()
        {
            Debug.Log("Head Charging");//for testing, comment out

            Movement();

            Boss.ChargeTimeHead -= Time.deltaTime;

            if (Boss.ChargeTimeHead <= 0)
            {
                Boss.ChargeTimeHead = CTimer;
                return new BreuBossHAttack();
            }


            return null;
        }

        private void Movement()
        {
            Vector3 DirToTarget = (Boss.Target.position - Boss.Head.position).normalized;
            Boss.VelocityHead += new Vector3(DirToTarget.x * Boss.AccelerationHead * Boss.AccelerationHead * Time.deltaTime, 0, 0);

            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3(0, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);
        }
    }
}