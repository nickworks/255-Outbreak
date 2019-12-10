using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powers
{
    public class GameManager : MonoBehaviour
    {
        public HealthController player;
        public PlayerShooting ammo;
        public HealthController boss;

        //these are used for the boss death
        public AudioSource audioSource;
        public AudioClip bossDeath;
        public GameObject bossBar;

        private bool healthDepleted = false;

        // Update is called once per frame
        void Update()
        {
            if (player.health == 0 && !healthDepleted) StartCoroutine(GameOver());
            else if (boss.health == 0 && !healthDepleted) StartCoroutine(GoToNextLevel());
        }

        IEnumerator GameOver()
        {
            healthDepleted = true;
            audioSource.Stop();

            yield return new WaitForSeconds(1.5f);
            Game.GameOver();

            yield break;
        }

        IEnumerator GoToNextLevel()
        {
            healthDepleted = true;

            bossBar.SetActive(false);

            //stop music, and play boss death sound effect
            audioSource.Stop();
            audioSource.PlayOneShot(bossDeath, 1f);

            //wait for animation to complete and go to next level
            yield return new WaitForSeconds(7f);
            Game.GotoNextLevel();

            yield break;
        }
    }

}
