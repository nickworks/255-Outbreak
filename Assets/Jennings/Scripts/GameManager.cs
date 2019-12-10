using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jennings {
    public class GameManager : MonoBehaviour {
        private bool gameEnded = false; // sets gameEnded to false

        public GameObject gameOverUI; // summons gameOverUI

        // Update is called once per frame
        void Update()
        {
            // Checks the health of DamageTaker (whether it be player or enemy)
            // If it is 0 then it runs EndGame
            if (DamageTaker.health == 0 && !gameEnded)
                EndGame();
                return;

        }

        // Activates the Game Over UI and is supposed to run Game.GameOver (failing to function)
        void EndGame()
        {

            gameOverUI.SetActive(true);
            Game.GameOver();
            
        }
    }
}
