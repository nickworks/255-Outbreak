using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class DamageTaker : MonoBehaviour
    {

        public float health = 100;

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0) Die();
        }

        void Die() // what to do when dying.
        {
            Destroy(gameObject);
        }


    }
}