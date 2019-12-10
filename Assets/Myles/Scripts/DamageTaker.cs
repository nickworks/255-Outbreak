using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Myles
{
    public class DamageTaker : MonoBehaviour
    {
        public float health = 100;

        public Text healthCounter;

        void Start()
        {

        }
      

        public void TakeDamage(float amount)
        {
            health -= amount;

            healthCounter.text = health.ToString();
          
            if (health <= 0) gameObject.BroadcastMessage("Die");
        }
        /// <summary>
        /// what to do when dying
        /// </summary>
        void Die()
        {
            Destroy(gameObject);
        }
    }
}
