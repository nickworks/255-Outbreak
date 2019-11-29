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

            Boss.ChargeTimeRight -= Time.deltaTime;

            if (Boss.ChargeTimeRight <= 0)
            {
                Boss.ChargeTimeRight = CTimer;
                return new BreuBossRAttack();
            }


            return null;
        }
    }
}