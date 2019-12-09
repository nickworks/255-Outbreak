using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wynalda
{
    public class HealthBAR : MonoBehaviour
    {
        public Slider healthBar;
        DamageTaker DamageTaker;

        void Start()
        {
            DamageTaker = GameObject.FindGameObjectWithTag("Enemy").GetComponent<DamageTaker>();
        }

        void Update()
        {
            healthBar.value = DamageTaker.health;
        }

    }
}