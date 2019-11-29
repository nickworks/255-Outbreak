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

            /// <summary>
            /// When the idle timer hits zero a random charge state is selected
            /// </summary>
            if (Boss.IdleTimer <= 0)
            {
                Boss.IdleTimer = ITimer;//reset idle timer to default chosen in inspector
                System.Random RND = new System.Random();
                int RandomNum = RND.Next(1, 4);

                Debug.Log(RandomNum);//for testing, comment out

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

        
    }
}