using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powers
{
    public class HealthController : MonoBehaviour
    {
        //set the starting health and the maximum health that can be held
        public int startingHealth = 100;
        public int maxHealth = 100;

        [Space(10)]

        //can the obejct regenerate health. if so, how to wait until it is regenerate and at what rate
        public bool canRegenerate = true;
        public float waitToRegen = 3;
        public float regenRate = 2;

        [Space(10)]

        public Material objectMaterial;
        private Color objectColor = new Color(1,1,1);

        [Space(10)]

        //the tag of colliders to hurt player, and the damage range for the attack to add randomness.
        public string hurtTag;
        public float damageMin;
        public float damageMax;

        [Space(10)]

        //these are used to play sound effects
        public AudioSource audioSource;
        public AudioClip damageSFX;


        //this variable holds the object's current health
        [HideInInspector]
        public float health = 100;

        //this variable is switched by outside objects if they hit
        [HideInInspector]
        public bool gotHit = false;
        [HideInInspector]
        public bool gotHitLast = false;

        private void Start()
        {
            //set health to the object's starting health, and clamp it to ensure it isn't above the max health.
            health = startingHealth;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        // Update is called once per frame
        void Update()
        {
            //if game is not paused, register hits and track health as per normal
            if (!Game.isPaused)
            {
                //if player hasn't been hit, regenerate health. if player was hit last frame, start the coroutine for regeneration.
                if (!gotHit && canRegenerate) health += (regenRate * Time.deltaTime);
                else if (gotHit && !gotHitLast) StartCoroutine(WaitForRegen());

                //clamp health to prevent oddities
                health = Mathf.Clamp(health, 0, maxHealth);

                //set gotHitLast to gotHit
                gotHitLast = gotHit;

                //set color to regular and apply to material
                objectColor = Color.Lerp(objectColor, new Color(1, 1, 1), 0.1f);
                objectMaterial.SetColor("_Color", objectColor);
            }
            
        }

        IEnumerator WaitForRegen()
        {
            //set color to red to indicate damage
            objectColor = new Color(1, 0, 0);

            //wait for regen
            yield return new WaitForSeconds(waitToRegen);
            gotHit = false;

            yield break;
        }

        private void OnTriggerEnter(Collider collider)
        {
            //if object gets hit by trigger with tag, remove health and play sound effect
            if (collider.tag == hurtTag && !gotHit)
            {
                gotHit = true;
                health -= Random.Range(damageMin, damageMax);

                audioSource.PlayOneShot(damageSFX, 0.7f);
            }
        }
    }

}
