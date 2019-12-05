using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossLAttack : BreuBossState
    {
        
        public override BreuBossState Update()
        {
            Debug.Log("Left Attacking");//for testing, comment out

            BossPunch BP = Boss.HandLeft.GetComponent<BossPunch>();
            if (BP != null)
            {
                if (BP.FinishedPunch == true)
                {
                    BP.FinishedPunch = false;

                    //Transition from Left Attack to Reset
                    return new BreuBossReset();
                }
                else
                {
                    BP.Punch();
                }
            }
            else
            {
                Debug.Log("Component missing on left hand!");
            }

            return null;
        }
    }
}