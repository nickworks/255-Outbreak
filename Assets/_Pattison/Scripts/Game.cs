using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static Game main { get; private set; }
    public static bool isPaused { get; private set; }

    static public void GameOver() {
        // TODO: show the "game over" screen
    }
    static public void GotoNextLevel() {
        // TODO: this
    }
    

    float prePauseTimescale = 1;

    static public List<ZoneInfo> Zones { get { return zones; } }
    static private List<ZoneInfo> zones = new List<ZoneInfo>() {
        Andrea.Zone.info,
        Breu.Zone.info,
        Caughman.Zone.info,
        Jennings.Zone.info,
        Myles.Zone.info,
        Petzak.Zone.info,
        Powers.Zone.info,
        Smith.Zone.info,
        Stralle.Zone.info,
        Takens.Zone.info,
        Wynalda.Zone.info
    };
    static private List<ZoneInfo> zonesUnplayed = new List<ZoneInfo>();
    public ZoneInfo currentZone { get; private set; }

    static public void Play(ZoneInfo zone) {
        if (main == null) return;
        main.WarpTo(zone);
    }
    static public bool InZone() {
        if (main == null) return false;
        return (main.currentZone.level != null);
    }

    void Awake() {
        if (main != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        main = this;

        SceneManager.activeSceneChanged += (Scene a, Scene b) => {
            if (!InZone()) return;
            if (!Pattison.LevelBannerController.main) return;
            Pattison.LevelBannerController.main.gameObject.SetActive(true);
        };
    }
    void Update() {
        if (Input.GetButtonDown("Pause") && InZone()) TogglePause();
        if (isPaused) return;
    }

    public void WarpRandom() {
        if (zonesUnplayed.Count == 0) zonesUnplayed = new List<ZoneInfo>(zones);
        if (zonesUnplayed.Count == 0) return;
        int index = Random.Range(0, zonesUnplayed.Count);
        WarpTo(zonesUnplayed[index]);
    }
    public void WarpTo(ZoneInfo zone) {

        Debug.LogFormat($"<size=18>warping to <b>{zone.level}</b></size>\n");
        SceneManager.LoadScene(zone.level, LoadSceneMode.Single);
        currentZone = zone;
        RemoveCurrentFromZoneList();

    }
    private void RemoveCurrentFromZoneList() {
        if (zonesUnplayed.Count == 0) zonesUnplayed = new List<ZoneInfo>(zones);
        if (zonesUnplayed.Count == 0) return;
        int index = zonesUnplayed.IndexOf(currentZone);
        zonesUnplayed.RemoveAt(index);
        Debug.Log($"<color=grey> ---- ({zonesUnplayed.Count-1} levels left in queue) ---- </color>\n\n");
        
    }
    // called by "Skip to Next" button
    public void Skip() {
        WarpRandom();
        SetPause(false);
    }
    public void TogglePause() {
        SetPause(null);
    }
    private void SetPause(bool? setTo = null) {

        bool pauseValue = !isPaused;
        if (setTo != null) {
            if (setTo == true) pauseValue = true;
            if (setTo == false) pauseValue = false;
        }
        isPaused = pauseValue;
        if (isPaused) prePauseTimescale = Time.timeScale;
        Time.timeScale = isPaused ? 0 : prePauseTimescale;
    }
    public void BackToMainMenu() {
        currentZone = new ZoneInfo();
        SceneManager.LoadScene("MainMenu");
        SetPause(false);
    }
    
}
