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

            Boss.ChargeTimeLeft -= Time.deltaTime;

            if (Boss.ChargeTimeLeft <= 0)
            {
                Boss.ChargeTimeLeft = CTimer;
                return new BreuBossLAttack();
            }


            return null;
        }
    }
}
