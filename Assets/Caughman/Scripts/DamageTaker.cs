using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class DamageTaker : MonoBehaviour
    {

        public float health = 100;


        /// <summary>
        /// Adjusting health based on ammount of damage taken
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0)gameObject.BroadcastMessage("Die");
        }//End TakeDamage
        
        /// <summary>
        /// What to do when dying
        /// </summary>
        void Die()
        {
            Destroy(gameObject);
        }
    }

}