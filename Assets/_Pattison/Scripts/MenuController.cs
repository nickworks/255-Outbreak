using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Transform menuMain;
    public MenuBuilder menuWarp;

    public EventSystem events;

    void Start() {
        if (events == null) events = GameObject.FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update() {
        Focus();
    }
    void Focus() {
        if (events == null) return;
        if (events.currentSelectedGameObject != null) return;

        if (menuWarp.gameObject.activeSelf) {
            menuWarp.Focus();
        } else {
            events.SetSelectedGameObject(events.firstSelectedGameObject);
        }
    }
    public void BttnPlay() {
        Game.main.WarpRandom();
    }
    public void BttnShowWarpMenu() {
        menuMain.gameObject.SetActive(false);
        menuWarp.gameObject.SetActive(true);
        events.SetSelectedGameObject(null);
    }
    public void BttnHideWarpMenu() {
        menuWarp.gameObject.SetActive(false);
        menuMain.gameObject.SetActive(true);
        events.SetSelectedGameObject(events.firstSelectedGameObject);
    }
    public void BttnExit() {
        Application.Quit();
    }
}
