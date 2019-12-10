using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda {
    public class HealthBars : MonoBehaviour //This script is made for the GUI that displays the player and enemy health!
    {

        public GameObject ThingTakingDamage; //This helps me call the Health variable found in DamageTaker
        DamageTaker damageTaker; //This helps me call the Health variable found in DamageTaker

        void Start()
        {
            if(ThingTakingDamage != null)//if the things that take damage(player/enemy) are not null, then it gets the component so i can call the health variable and display it
            {
                damageTaker = ThingTakingDamage.GetComponent<DamageTaker>(); //gets a link to damage taker script
            }
        }

        void OnGUI() //This draws the words and shows the health on the screen.
        {
            GUI.Label(new Rect(20, 20, 230, 160), "Player Health: " + damageTaker.health); // this displays the players health and updates in real time on the GUI!       
        }
    }
}