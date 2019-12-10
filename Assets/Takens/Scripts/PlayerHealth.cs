using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Class for handeling the health of the player
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        /// <summary>
        /// Access to the UI representation of the players health
        /// </summary>
        public GameObject healthBar;

        /// <summary>
        /// The transform of the players health bar
        /// </summary>
        private RectTransform t;

        /// <summary>
        /// The players health on a scale of 0-100
        /// </summary>
        public float health = 100;

        /// <summary>
        /// Boolean for wether or not the player is dead
        /// </summary>
        private bool isDead = false;
        
        /// <summary>
        /// This method is called once on startup
        /// </summary>
        void Start()
        {
            t = healthBar.GetComponent<RectTransform>();
        }

        /// <summary>
        /// This method is called onc per frame
        /// </summary>
        void Update()
        {
            if (isDead) return;
            if (health > 100) health = 100;
            if (health < 0 || health == 0) { 
                
                health = 0;
                StartCoroutine(EndGame());
                isDead = true;
            }
            t.localScale = new Vector3((health / 100), 1, 1);
        }

        /// <summary>
        /// If there was a collision with the players body
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Damage d = other.GetComponent<Damage>();
            if (d == null) return;

            if (!d.friendly) //checks to see if the bullet belongs to the player or the enemy
                health -= d.damage;

            Destroy(d.gameObject);//destroy the bullet on collision
        }

        /// <summary>
        /// This is called when the player dies after their health reaches 0
        /// Triggers game over
        /// </summary>
        /// <returns></returns>
        IEnumerator EndGame()
        {
            //play effect
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.GetComponent<PlayerShoot>().enabled = false;
            yield return new WaitForSeconds(.5f);
            Game.GameOver();
            Debug.Log("GAME OVER!");
            yield return null;
        }
    }
}