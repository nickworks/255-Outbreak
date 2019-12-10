using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda {
    public class HealthBars : MonoBehaviour
    {

        public GameObject ThingTakingDamage;
        DamageTaker damageTaker;

        void Start()
        {
            if(ThingTakingDamage != null)
            {
                damageTaker = ThingTakingDamage.GetComponent<DamageTaker>();
            }
        }

        void OnGUI()
        {
          GUI.Label(new Rect(20, 20, 230, 160), "Player Health: " + damageTaker.health);            
        }
    }
}