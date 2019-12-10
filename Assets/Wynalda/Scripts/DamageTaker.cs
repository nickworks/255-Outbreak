using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    
    public class DamageTaker : MonoBehaviour
    {

        private HealthBars healthBar; // this allows me to call the health float in other scripts.
        public float health = 250; // health of player/enemy. changed in the inspector.

        private void Awake()
        {
            healthBar = GameObject.FindObjectOfType<HealthBars>();//This allows me to call the "Health" float in this script in my HealthBars and HealthBarsForEnemy script(s).
        }

        public void TakeDamage(float amount) // This is what to do when something takes damage.
        {
            health -= amount; // This makes the health variable go down the amount that the "amount" variable dictates. This way different bullets/guns do different amounts of damage.
            if (health <= 0) Die();  // health below 0? Death.

      

        }

        void Die() // what to do when dying.
        {
            Destroy(gameObject); //The Player/Enemy has died and their gameObject is removed as a result!
            Game.GameOver();//Game Over! Player died!
        }



    }
}