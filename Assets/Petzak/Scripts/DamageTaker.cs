using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Class for taking damage
    /// </summary>
    public class DamageTaker : MonoBehaviour
    {
        /// <summary>
        /// Time to move to next level, once boss or player is dead
        /// </summary>
        public float timeToNextLevel = 120;
        /// <summary>
        /// If player or boss has died
        /// </summary>
        public bool dying = false;
        /// <summary>
        /// Default health of player and boss
        /// </summary>
        public float health = 200;

        /// <summary>
        /// Called every frame.
        /// Goes to next level if player or boss dies.
        /// </summary>
        void Update()
        {
            if (dying)
                timeToNextLevel--;

            if (timeToNextLevel <= 0)
                Game.GotoNextLevel();
        }

        /// <summary>
        /// Updates health on the hud and reduces health.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            if (gameObject.ToString().Contains("Spider"))
                HUD.instance.ReduceBossHealth(amount);
            else
                HUD.instance.ReducePlayerHealth(amount);

            health -= amount;
            if (health <= 0)
                gameObject.BroadcastMessage("Die");
        }

        /// <summary>
        /// What to do when dying.
        /// </summary>
        void Die()
        {
            dying = true;
        }
    }
}