using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBossHAttack : BreuBossState
    {
        public override BreuBossState Update()
        {
            Debug.Log("Head Attacking");//for testing, comment out

            return new BreuBossIdle();
        }
    }
}