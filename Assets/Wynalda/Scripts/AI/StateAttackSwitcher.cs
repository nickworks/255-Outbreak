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
            //When it's done pursuing, the boss randomly enters 1 of 3 attack modes.
            int attackState = Random.Range(0, 3); // random between the 3 attacks.
            if(attackState == 0)
            {
                return new StateCircleAttack(); //circle attack, bullets all around him
            }
            if (attackState == 1)
            {
                return new StateStrongAttack(); // strong attack, 5 bullets in one, pack a big punch.
            }
            if (attackState == 2)
            {
                return new StateBasicAttack(); // basic attack, but quick shooting time, so can often come out and then immediately another attack as well. 
            }


            return null; // otherwise null
        }
    }
}