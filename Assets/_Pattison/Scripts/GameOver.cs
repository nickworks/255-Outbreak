using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void GiveUp() {
        Game.main.BackToMainMenu();
    }
    public void KeepOnTruckin() {
        Game.GotoNextLevel();
    }
}
