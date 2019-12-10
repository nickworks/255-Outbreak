using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breu
{
    public class BreuBossRCharge : BreuBossState
    {
        float CTimer;//hold the max charge time

        Renderer rend;//used to change color when charging
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeRight;

            rend = Boss.HandRight.GetComponent<Renderer>();
        }

        public override BreuBossState Update()
        {
            //Debug.Log("Right Charging");//for testing, comment out

            rend.material.color = Color.Lerp(Color.white, Boss.ChargeColor, .75f);//transistions color from white to magenta

            movement();

            Boss.ChargeTimeRight -= Time.deltaTime;



            //transition from Charging to Attack
            if (Boss.ChargeTimeRight <= 0)//resets charge time and color then switches to Right Attack
            {
                Boss.ChargeTimeRight = CTimer;// resest current time to max
                rend.material.color = Color.white;//reset color to white
                return new BreuBossRAttack();
            }


            return null;
        }

        /// <summary>
        /// Sets velovity for each part and applies it
        /// </summary>
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