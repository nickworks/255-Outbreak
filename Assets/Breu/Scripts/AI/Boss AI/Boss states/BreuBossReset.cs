using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Breu {
    public class BreuBossReset : BreuBossState
    {
        float RstTimer;

        public override void OnBegin(BreuBossController boss)
        {
            base.OnBegin(boss);

            RstTimer = Boss.ResetTimer;
        }

        // Update is called once per frame
        public override BreuBossState Update()
        {
            Boss.ResetTimer -= Time.deltaTime;

            Vector3 DirToTarget = (Boss.StartRight - Boss.HandRight.position).normalized;
            if (Mathf.Abs(DirToTarget.z) > .6)
            {
                Boss.VelocityRight += new Vector3(0, 0, DirToTarget.z * Boss.AccelerationRight * Boss.AccelerationRight * Time.deltaTime);
            }

            DirToTarget = (Boss.LeftStartPoint.position - Boss.HandLeft.position).normalized;
            if (Mathf.Abs(DirToTarget.z) > .6 || Mathf.Abs(DirToTarget.x) > .6)
            {
                Boss.VelocityLeft += new Vector3(DirToTarget.x * Mathf.Sqrt(Boss.AccelerationLeft * 500)  * Time.deltaTime, 0, DirToTarget.z * Boss.AccelerationLeft * Boss.AccelerationLeft * Time.deltaTime);
            }

            DirToTarget = (Boss.StartHead - Boss.Head.position).normalized;
            if (Mathf.Abs(DirToTarget.x) > .6)
            {
                Boss.VelocityHead += new Vector3(DirToTarget.x * Boss.AccelerationHead * Boss.AccelerationHead * Time.deltaTime, 0, 0);
            }
            //Transition from Reset to Idle
            if (Boss.ResetTimer <= 0)
            {
                Boss.ResetTimer = RstTimer;
                return new BreuBossIdle();
            }

            return null;
        }
    }
}