using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pattison {
    public class LevelBannerController : MonoBehaviour {

        public Text buttonText;
        public Text levelText;
        public Text nameText;
        public RectTransform background;

        public float backgroundTransitionTime = 0.5f;
        public AnimationCurve backgroundTransitionCurve;
        float timerBackground;

        public RectTransform pauseMenu;
        public EventSystem eventSystem;

        static public LevelBannerController main { get; private set; }

        void Start() {

            if (main != null) {
                Destroy(gameObject);
                return;
            }

            main = this;
            DontDestroyOnLoad(gameObject);
            if (eventSystem == null) eventSystem = GameObject.FindObjectOfType<EventSystem>();
        }
        void Update() {
            if (Game.main) {
                buttonText.text = Game.isPaused ? "Play" : "Pause";
                nameText.text = Game.main.currentZone.creator;
                levelText.text = Game.main.currentZone.zoneName;

                if (!Game.InZone()) {
                    timerBackground = 0;
                    gameObject.SetActive(false);
                }
            }
            AnimateBackground();
            UpdatePauseMenu();
            Focus();
        }
        private void AnimateBackground() {
            timerBackground += Game.isPaused ? Time.unscaledDeltaTime : -Time.unscaledDeltaTime;
            timerBackground = Mathf.Clamp(timerBackground, 0, backgroundTransitionTime);
            if (backgroundTransitionTime < 0) backgroundTransitionTime = 0.5f;
            float p = timerBackground / backgroundTransitionTime;
            p = 1 - backgroundTransitionCurve.Evaluate(p);
            background.anchorMin = new Vector2(0, p);
        }
        private void UpdatePauseMenu() {
            if (pauseMenu) {
                if (Game.isPaused && !pauseMenu.gameObject.activeSelf) {
                    pauseMenu.gameObject.SetActive(true);
                    eventSystem.SetSelectedGameObject(null);
                }
                if (!Game.isPaused && pauseMenu.gameObject.activeSelf) {
                    pauseMenu.gameObject.SetActive(false);
                }
            }
        }
        void Focus() {
            if (eventSystem == null) return;
            if (eventSystem.currentSelectedGameObject != null) return;
            if (eventSystem.firstSelectedGameObject == null) return;
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
    }
}