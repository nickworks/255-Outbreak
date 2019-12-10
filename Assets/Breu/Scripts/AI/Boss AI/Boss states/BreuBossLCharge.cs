using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossLCharge : BreuBossState
    {
        float CTimer;//hold the max charge time

        Renderer rend;//used to change color when charging

        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeLeft;

            rend = Boss.HandLeft.GetComponent<Renderer>();
        }

        public override BreuBossState Update()
        {
            //Debug.Log("Left Charging");//for testing, comment out

            rend.material.color = Color.Lerp(Color.white, Boss.ChargeColor , 0.75f);//transistions color from white to magenta

            Movement();//moves all parts

            Boss.ChargeTimeLeft -= Time.deltaTime;

            if (Boss.ChargeTimeLeft <= 0)//resets charge time and color then switches to Left Attack
            {
                Boss.ChargeTimeLeft = CTimer;// resest current time to max
                rend.material.color = Color.white;//reset color to white
                return new BreuBossLAttack();
            }


            return null;
        }

        /// <summary>
        /// Sets velovity for each part and applies it
        /// </summary>
        private void Movement()//Moves each part of boss
        {
            //moves left part
            Vector3 DirToTarget = (Boss.Target.position - Boss.HandLeft.position).normalized;
            Boss.VelocityLeft += new Vector3((Boss.LeftStartPoint.position - Boss.HandLeft.position).x * Boss.AccelerationLeft * Time.deltaTime, 0, DirToTarget.z * Boss.AccelerationLeft * Boss.AccelerationLeft * Time.deltaTime);

            //moves right part
            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            //move head part
            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }
    }
}
