using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda {
    public class HealthBarsForEnemy : MonoBehaviour //This script is made for the GUI that displays the player and enemy health! A duplicate script used for the enemy.
    {

        public GameObject ThingTakingDamage2; //This helps me call the Health variable found in DamageTaker
        DamageTaker damageTaker2; //This helps me call the Health variable found in DamageTaker

        void Start()
        {
            if(ThingTakingDamage2 != null)//if the things that take damage(player/enemy) are not null, then it gets the component so i can call the health variable and display it
            {
                damageTaker2 = ThingTakingDamage2.GetComponent<DamageTaker>();//gets a link to damage taker script
            }          
        }

        void OnGUI()//This draws the words and shows the health on the screen.
        {
           GUI.Label(new Rect(620, 20, 1000, 1000), "Enemy Health: " + damageTaker2.health); // this displays the players health and updates in real time on the GUI!       
        }
    }
    
}