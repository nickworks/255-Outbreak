using System.Collections;
using UnityEngine;

namespace Powers
{
    public class PlayerShooting : MonoBehaviour
    {
        //the clip size 
        public float clipSize = 6;
        [HideInInspector]
        public float currentClip = 6;

        [Space(10)]
        public float waitBetweenBullets = 0.2f;
        public float waitToReload = 4f;

        [Space(10)]

        public GameObject basicBullet;
        public Transform projectileSpawnPoint;
        public GameObject rotationIndicator;

        private float bulletCooldown = 0.2f;
        private float currentVelocity;
        
        [HideInInspector]
        public bool canShoot = true;

        //these are used to play sound effects
        private AudioSource audioSource;
        public AudioClip shootSFX;
        public AudioClip reloadSFX;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentClip = clipSize;
        }


        void Update()
        {
            //if game is not paused, allow player to shoot
            if (!Game.isPaused)
            {
                //subtract the cooldown and clamp it.
                if (bulletCooldown > 0) bulletCooldown -= Time.deltaTime;
                bulletCooldown = Mathf.Clamp(bulletCooldown, 0, bulletCooldown);

                //if fire button is down and there's ammo, fire.
                if (Input.GetButtonDown("Fire1") && currentClip != 0 && canShoot) ShootBullet();
                //otherwise, if there is no ammo and the fire button is down, reload.
                else if (currentClip == 0) StartCoroutine(Reload());
            }

        }

        private void ShootBullet()
        {
            //create the bullet
            Instantiate(basicBullet, projectileSpawnPoint.position, rotationIndicator.transform.rotation);
            audioSource.PlayOneShot(shootSFX, 0.9f);

            //subtract from current ammo and reset bullet cooldown;
            currentClip--;
            bulletCooldown = waitBetweenBullets;
        }

        IEnumerator Reload()
        {
            //this is to ensure the player cannot shoot or accidently restart reloading while the process is occurring
            canShoot = false;
            audioSource.PlayOneShot(reloadSFX, 0.9f);

            //transitions the current clip size to the max clip size
            while (currentClip != clipSize)
            {
                currentClip += (clipSize/waitToReload) * Time.deltaTime;
                currentClip = Mathf.Clamp(currentClip, 0, clipSize);

                yield return null;
            }

            //once this is done, allow player to shoot again, reloading is done
            canShoot = true;

            yield break;
        }
    }
}