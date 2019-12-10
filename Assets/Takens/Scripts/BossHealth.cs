using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Takens
{
    /// <summary>
    /// This class is for handeling the health of the enemy
    /// </summary>
    public class BossHealth : MonoBehaviour
    {
        /// <summary>
        /// Reference to the particle system that is played when the enemy is defeated
        /// </summary>
        public ParticleSystem defeat;

        /// <summary>
        /// Reference to the health bar object of the enemy
        /// </summary>
        public GameObject healthBar;

        /// <summary>
        /// Reference to the transform of the healthbar
        /// </summary>
        private RectTransform t;

        /// <summary>
        /// The actual enemy's health on a scale of 0-100
        /// </summary>
        public float health = 100;


        /// <summary>
        /// Boolean of wether or not the enemy is dead
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
        /// This method is called once per frame
        /// </summary>
        void Update()
        {
            if (isDead) return;
            if (health > 100) health = 100;
            if (health < 0 || health == 0)
            {

                health = 0;
                StartCoroutine(NextLevel());
                isDead = true;
            }
            t.localScale = new Vector3((health / 100),1,1);
        }

        /// <summary>
        /// This method is called when an object collides with the enemy's body
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Damage d = other.GetComponent<Damage>();
            if (d == null) return;

           if (d.friendly)
                health -= d.damage;

           //does not delete bullets so that boomerangs can hit twice
        }

        /// <summary>
        /// Called when the enemy's health reaches 0 and the enemy is defeated
        /// </summary>
        /// <returns></returns>
        IEnumerator NextLevel()
        {
            //play effect
            this.gameObject.GetComponent<EnemyController>().enabled = false;
            this.gameObject.GetComponent<SpaceShipHover>().enabled = false;

            defeat.Play();
            yield return new WaitForSeconds(1.2f);
            Game.GotoNextLevel();
            Debug.Log("NEXT LEVEL");
            yield return null;
        }

    }
}