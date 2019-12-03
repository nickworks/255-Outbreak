using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myles
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }

        public Slider healthbar;

        void Start()
        {
            MaxHealth = 20f;
            //Resets health upon start of game
            CurrentHealth = MaxHealth;

            healthbar.value = CalculateHealth();
        }

        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                DealDamage(6);
            }

            void DealDamage(float damageValue)
            {
                CurrentHealth -= damageValue;
                healthbar.value = CalculateHealth();
                
                if (CurrentHealth <= 0)
                {
                    Destroy(gameObject);
                    Game.GameOver();
                }
                
            }
        }
        float CalculateHealth()
        {
            return CurrentHealth / MaxHealth;
        }
    }
}
