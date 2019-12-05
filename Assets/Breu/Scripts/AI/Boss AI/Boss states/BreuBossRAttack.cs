using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossRAttack : BreuBossState
    {
        public override BreuBossState Update()
        {
            //Debug.Log("Right Attacking");//for testing, comment out
            BreuBossShoot BS = Boss.HandRight.GetComponent<BreuBossShoot>();
            if (BS != null)
            {

                if (BS.FinishedAttack == true)
                {
                    BS.FinishedAttack = false;
                    //Transition from Right Attack to Reset
                    return new BreuBossReset();
                }

                BS.Attack();

            }
            else
            {
                Debug.Log("Component Missing on right hand!");
            }

            return null;

        }
    }
}