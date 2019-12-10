using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Petzak
{
    /// <summary>
    /// Controls hud elements
    /// </summary>
    public class HUD : MonoBehaviour
    {
        /// <summary>
        /// Global hud instance
        /// </summary>
        public static HUD instance = new HUD();

        /// <summary>
        /// private constructor
        /// </summary>
        private HUD ()
        {

        }

        /// <summary>
        /// Currently selected weapon
        /// </summary>
        public PlayerShooting.WeaponType weapon = PlayerShooting.WeaponType.PeaShooter;
        /// <summary>
        /// UI element for player health
        /// </summary>
        public GameObject playerBar;
        /// <summary>
        /// UI element for boss health
        /// </summary>
        public GameObject bossBar;
        /// <summary>
        /// UI element for current weapon
        /// </summary>
        public GameObject currentWeapon;
        /// <summary>
        /// UI element for player ammo
        /// </summary>
        public GameObject ammo;
        /// <summary>
        /// Boss current health
        /// </summary>
        public float bossHealth = 200;
        /// <summary>
        /// Player current health
        /// </summary>
        public float playerHealth = 200;
        /// <summary>
        /// Current ammo count
        /// </summary>
        public float ammoCount;

        /// <summary>
        /// Called on start.
        /// Set instance variables.
        /// </summary>
        void Start()
        {
            instance.playerBar = playerBar;
            instance.bossBar = bossBar;
            instance.ammo = ammo;
            instance.currentWeapon = currentWeapon;
        }

        /// <summary>
        /// Removes an amount from the players health bar.
        /// </summary>
        /// <param name="amount"></param>
        public void ReducePlayerHealth(float amount)
        {
            instance.playerHealth -= amount;
            RectTransform rt = instance.playerBar.GetComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, instance.playerHealth);
            rt.transform.localPosition -= new Vector3(amount - amount / 2, 0);
        }

        /// <summary>
        /// Removes an amount from the bosses health bar.
        /// </summary>
        /// <param name="amount"></param>
        public void ReduceBossHealth(float amount)
        {
            instance.bossHealth -= amount;
            RectTransform rt = instance.bossBar.GetComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, instance.bossHealth);
            rt.transform.localPosition -= new Vector3(amount - amount / 2, 0);
        }

        /// <summary>
        /// Decerements ammo and updates UI element
        /// </summary>
        public void UpdateAmmo()
        {
            ammoCount--;
            instance.ammo.GetComponent<Text>().text = ammoCount.ToString();
        }

        /// <summary>
        /// Updates currently selected weapon.
        /// </summary>
        /// <param name="weapon"></param>
        public void UpdateWeapon(PlayerShooting.WeaponType weapon)
        {
            if (weapon == PlayerShooting.WeaponType.PeaShooter)
            {
                ammoCount = 0;
                instance.ammo.GetComponent<Text>().text = "*";
                instance.currentWeapon.GetComponent<Text>().text = "Pea Shooter";
            }
            else if (weapon == PlayerShooting.WeaponType.AutoRifle)
            {
                ammoCount = 50;
                instance.ammo.GetComponent<Text>().text = "50";
                instance.currentWeapon.GetComponent<Text>().text = "Auto Rifle";
            }
            else if (weapon == PlayerShooting.WeaponType.TripleShot)
            {
                ammoCount = 20;
                instance.ammo.GetComponent<Text>().text = "20";
                instance.currentWeapon.GetComponent<Text>().text = "Triple Shot";
            }
            instance.weapon = weapon;
        }
    }
}