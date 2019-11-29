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
            Debug.Log("Right Attacking");//for testing, comment out

            return new BreuBossIdle();
        }
    }
}