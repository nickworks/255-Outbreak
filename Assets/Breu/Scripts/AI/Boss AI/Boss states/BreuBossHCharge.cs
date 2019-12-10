using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossHCharge : BreuBossState
    {
        float CTimer;//will hold that max charge time
        Renderer rend;//used to change color when charging
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            CTimer = Boss.ChargeTimeHead;
            rend = Boss.Head.GetComponent<Renderer>();

            /*
            if (Boss.HeadAttackWarningRenderer != null)
            {
                Boss.HeadAttackWarningRenderer.enabled = true;
            }
            */
        }

        public override BreuBossState Update()
        {
            //Debug.Log("Head Charging");//for testing, comment out

            rend.material.color = Color.Lerp(Color.white, Boss.ChargeColor, .75f);//transistions color from white to magenta

            Movement();

            Boss.ChargeTimeHead -= Time.deltaTime;

            if (Boss.ChargeTimeHead <= 0)//resets charge time and color then switches to Head Attack
            {
                Boss.ChargeTimeHead = CTimer;// resest current time to max
                rend.material.color = Color.white;//reset color to white
                return new BreuBossHAttack();
            }
            /*
            if (Boss.HeadAttackWarningRenderer != null)
            {
                Boss.HeadAttackWarningRenderer.material.color = Color.yellow;
            }
            */
            return null;
        }

        /// <summary>
        /// Sets velovity for each part and applies it
        /// </summary>
        private void Movement()
        {
            //head
            Vector3 DirToTarget = (Boss.Target.position - Boss.Head.position).normalized;
            Boss.VelocityHead += new Vector3(DirToTarget.x * Boss.AccelerationHead * Boss.AccelerationHead * Time.deltaTime, 0, 0);

            //right
            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            //left
            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3(0, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);
        }
    }
}