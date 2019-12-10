using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    
    // Is intended to check to see if the game is paused or not. 
    // If it is then time in game should stop. If it isn't it should
    // Resume at a normal speed.
    // Update is called once per frame
    void Update()
    {
        if (Game.isPaused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
        
    }
}
