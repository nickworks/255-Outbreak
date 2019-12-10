using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Breu
{
    public class BreuReloadMeter : MonoBehaviour
    {
        public GameObject MainBody;//main gameobject in a  prefab
        public Canvas ReloadBar;//canvas for reload bar

        private Image Foreground;

        BreuPlayerShoot ReloadSource;

        /// <summary>
        /// checks if mainbody and reload bar are not null, sets damagesource and foreground
        /// </summary>
        void Start()
        {

            if (MainBody != null)
            {
                ReloadSource = MainBody.GetComponent<BreuPlayerShoot>();
            }
            if (ReloadBar != null)
            {
                Foreground = ReloadBar.GetComponent<Image>();
            }
        }

        /// <summary>
        /// if reload source is not null, sets percent of current reload time compared to max reload time.
        /// if foreground is not null, sets fill amout to percent found above
        /// </summary>
        void Update()
        {
            float ReloadPercent = 1;
            if (ReloadSource != null)
            {
                ReloadPercent = ReloadSource.CooldownToShoot / ReloadSource.MaxCooldown;
            }
            if (Foreground != null)
            {
                Foreground.fillAmount = ReloadPercent;
            }

        }

        /// <summary>
        /// roates reload bar to face towards camera
        /// </summary>
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}