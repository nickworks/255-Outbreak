using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andrea
{
    /// <summary>
    /// Holds health of the object and UI for the health bar
    /// </summary>
    public class DamageTaker : MonoBehaviour
    {
        /// <summary>
        /// The maximum health of the object
        /// </summary>
        public float maxHealth = 100;

        /// <summary>
        /// The current health of the object
        /// </summary>
        public float health;


        AudioSource source; // Damage SFX

        /// <summary>
        /// The image component of the health bar used as a left-hand fill
        /// </summary>
        public Image healthBar;


        /// <summary>
        /// Applies the specified amount of damage to the game object.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            health -= amount;

            if (source != null)
            {
                source.Play();
            }

            if (health <= 0)
            {
                gameObject.BroadcastMessage("Die");
            }
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {
            if (healthBar != null)
            {
                healthBar.fillAmount = health / maxHealth; //Update healthbars

                //Adjust color of the bar depending on hitpoints remaining
                if (healthBar.fillAmount >= .5)
                {
                    healthBar.color = Color.green;
                }
                else if (healthBar.fillAmount >= .25)
                {
                    healthBar.color = Color.yellow;
                }
                else
                {
                    healthBar.color = Color.red;
                }
            }
        }

        /// <summary>
        /// Called upon instantiation
        /// </summary>
        void Start()
        {
            source = GetComponent<AudioSource>();
            health = maxHealth;
        }

        /// <summary>
        /// What to do when dying.
        /// </summary>
        void Die()
        {
            Destroy(gameObject);
        }
    }
}