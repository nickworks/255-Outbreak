using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Jennings {
    public class DamageTaker : MonoBehaviour {

        public float startHealth = 100; // Damage Taker's original health
        public static float health; // Their current health
        public bool isPlayer = false; // checks to see if the damage taker is the player
        public bool isEnemy = false; // checks to see if the damage taker is the enemy

        public Image healthBar; // calls upon health bar (to change based on health)

        // Start is called before the first frame update
        void Start()
        {
            // Establishes Health
            health = startHealth / 2;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        // Does the action of taking damage, takes away health from GameObject
        public void TakeDamage(float amount)
        {
            health -= amount;

            healthBar.fillAmount = health / startHealth * 2;

            if (health <= 0) gameObject.BroadcastMessage("Die");
        }
        // what to do when dying
        void Die()
        {
            // If it is a player it should call game over
            if (isPlayer)
            {
                Game.GameOver();
            }
            // If it is an enemy it should go to next level (not working for some reason)
            else if (isEnemy)
            {
                Game.GotoNextLevel();
            }
            // Destroys game object
            Destroy(gameObject);
            
            
            
        }

    }
}
