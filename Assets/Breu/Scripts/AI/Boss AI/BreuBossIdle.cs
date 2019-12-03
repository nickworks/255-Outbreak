using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu {
    public class BreuBossIdle : BreuBossState
    {
        float ITimer;
        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            ITimer = Boss.IdleTimer;
        }

        public override BreuBossState Update()
        {
            Debug.Log("idling");//for testing, comment out

            Boss.IdleTimer -= Time.deltaTime;

            Movement();

            /// <summary>
            /// When the idle timer hits zero a random charge state is selected
            /// </summary>
            if (Boss.IdleTimer <= 0)
            {
                Boss.IdleTimer = ITimer;//reset idle timer to default chosen in inspector
                System.Random RND = new System.Random();
                int RandomNum = RND.Next(1, 4);

                //Debug.Log(RandomNum);//for testing, comment out

                if (RandomNum == 1)
                {
                    return new BreuBossLCharge();
                }
                else if (RandomNum == 2)
                {
                    return new BreuBossRCharge();
                }
                else
                {
                    return new BreuBossHCharge();
                }                
            }

            return null;
        }

        void Movement()
        {
            float RightDir = -Mathf.Cos(Time.fixedTime) * Boss.MovementRangeRight;
            Boss.VelocityRight += new Vector3(0, 0, RightDir * Boss.AccelerationRight * Time.deltaTime);

            float LeftDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeLeft;
            Boss.VelocityLeft += new Vector3(0, 0, LeftDir * Boss.AccelerationLeft * Time.deltaTime);

            float HeadDir = Mathf.Cos(Time.fixedTime) * Boss.MovementRangeHead;
            Boss.VelocityHead += new Vector3(HeadDir * Boss.AccelerationHead * Time.deltaTime, 0, 0);
        }

        
    }
}