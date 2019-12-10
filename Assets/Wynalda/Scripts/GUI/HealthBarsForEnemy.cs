using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda {
    public class HealthBarsForEnemy : MonoBehaviour
    {

        public GameObject ThingTakingDamage2;
        DamageTaker damageTaker2;

        void Start()
        {
            if(ThingTakingDamage2 != null)
            {
                damageTaker2 = ThingTakingDamage2.GetComponent<DamageTaker>();
            }
        }

        void OnGUI()
        {
           GUI.Label(new Rect(620, 20, 230, 160), "Enemy Health: " + damageTaker2.health);            
        }
    }
}