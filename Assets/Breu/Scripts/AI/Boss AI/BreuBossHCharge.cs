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

            Boss.ChargeTimeHead -= Time.deltaTime;

            if (Boss.ChargeTimeHead <= 0)
            {
                Boss.ChargeTimeHead = CTimer;
                return new BreuBossHAttack();
            }


            return null;
        }
    }
}