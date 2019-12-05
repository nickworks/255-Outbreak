using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuDamageTake : MonoBehaviour
    {
        public float MaxHealth = 100;
        public float CurrentHealth;


        public bool IsShielded = false;
        public float ShieldTimer = 3;
        private float ShieldRemaining;

        // Start is called before the first frame update
        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsShielded == true)
            {
                Debug.Log("Shield up");//for testing, comment out
                ShieldRemaining -= Time.deltaTime;
                if (ShieldRemaining <= 0)
                {
                    IsShielded = false;
                    Debug.Log("ShieldDown");//for testing, comment out
                }
            }
        }

        public void TakeDamage(float Damage)
        {
            if (IsShielded == false)
            {
                CurrentHealth -= Damage;
                if (CurrentHealth <= 0)
                {
                    OnDeath();
                }
                else
                {
                    IsShielded = true;
                    ShieldRemaining = ShieldTimer;
                }
            }
        }


        //when CurrentHealth reaches or is below zero
        void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}