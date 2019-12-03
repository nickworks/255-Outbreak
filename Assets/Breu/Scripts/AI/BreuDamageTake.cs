using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuDamageTake : MonoBehaviour
    {
        public float MaxHealth = 100;
        public float CurrentHealth;

        // Start is called before the first frame update
        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float Damage)
        {
            CurrentHealth -= Damage;
            if (CurrentHealth <= 0)
            {
                OnDeath();
            }
        }


        //when CurrentHealth reaches or is below zero
        void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}