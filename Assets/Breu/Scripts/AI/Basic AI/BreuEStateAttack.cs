using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuEStateAttack : EnemyState
    {

        int ammo = 0;
        int maxAmmo = 5;
        public override void OnBegin(BreuEnemyController enemy)
        {
            base.OnBegin(enemy);

            ammo = maxAmmo;
        }
        // Update is called once per frame
        public override EnemyState Update()
        {


            



        ////////// Behavior
        Debug.Log("ATTACK!");

            // attack target
            if (Enemy.WeaponWait <= 0 && ammo > 0)
            {
                Enemy.FireAttack();
                ammo--;
                Debug.Log(ammo);
            }



            ////////// Transitions

            // transition : from ATTACK to PURSuE  -  if distance is greater than attack distance
            Vector3 ToTarget = Enemy.Target.position - Enemy.transform.position;
            float SqrDis = ToTarget.sqrMagnitude;

            if (SqrDis > Enemy.AttackDistanceThreshold * Enemy.AttackDistanceThreshold)
            {
                return new BreuEStatePursue();
            }


            // transition : from ATTACK to RELOAD  -  if amme <= 0
            if (ammo <= 0)
            {
                return new BreuEStateReload();
            }


            return null;
        }
    }
}