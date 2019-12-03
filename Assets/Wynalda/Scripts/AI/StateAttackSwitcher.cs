using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class StateAttackSwitcher : EnemyState
    {

        public override EnemyState Update()
        {                   
            ///////// TRANSITION TO OTHER STATES:

            //switch to pursue
            Vector3 toAttackTarget = enemy.attackTarget.position - enemy.transform.position;
            float disSqr = toAttackTarget.sqrMagnitude;

            if(disSqr > enemy.attackDistanceThreshold * enemy.attackDistanceThreshold)
            {
                return new StatePursue();
            }
            int attackState = Random.Range(0, 3);
            if(attackState == 0)
            {
                return new StateBasicAttack();
            }
            if (attackState == 1)
            {
                return new StateTripleAttack();
            }
            if (attackState == 2)
            {
                return new StateBasicAttack();
            }


            return null;
        }
    }
}