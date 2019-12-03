using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class DamageTaker : MonoBehaviour
    {
        public float health = 100;
      

        public void TakeDamage(float amount)
        {
            health -= amount;

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
