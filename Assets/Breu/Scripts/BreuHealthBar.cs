using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Breu
{
    public class BreuHealthBar : MonoBehaviour
    {
        public GameObject MainBody;//the parent in a prefab
        public Canvas HealthBar;// the health bar canvas

        public AudioSource HitSound;

        private Image Foreground;

        private float PreviousHealth = 1;
        private float CurrentHealth;

        BreuDamageTake DamageSource;

        /// <summary>
        /// checks if mainbody and health bar are not null, sets damagesource and foreground
        /// </summary>
        void Start()
        {

            if (MainBody != null)
            {
                DamageSource = MainBody.GetComponent<BreuDamageTake>();
            }
            if(HealthBar != null)
            {
                Foreground = HealthBar.GetComponent<Image>();
            }

        }

        /// <summary>
        /// if damage source is not null, sets percent of current health compared to max health.
        /// if foreground is not null, sets fill amout to percent found above
        /// </summary>
        void Update()
        {
            if (DamageSource != null)
            {
                CurrentHealth = DamageSource.CurrentHealth / DamageSource.MaxHealth;
            }
            if(Foreground != null)
            {
                Foreground.fillAmount = CurrentHealth;
            }
            if (CurrentHealth < PreviousHealth &&PreviousHealth != 0)
            {
                HitSound.Play();
                PreviousHealth = CurrentHealth;
            }

        }

        /// <summary>
        /// roates healthbar to face towards camera
        /// </summary>
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}