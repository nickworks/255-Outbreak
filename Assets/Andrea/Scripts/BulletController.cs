using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// Controls bullet behavior
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        /// <summary>
        /// Speed in m/s
        /// </summary>
        public float speed = 10;

        /// <summary>
        /// The lifespan of the projectile in seconds
        /// </summary>
        public float lifespan = 3;

        /// <summary>
        /// The amount of damage the projectile does
        /// </summary>
        public float damageAmount = 20;

        /// <summary>
        /// A flag for explosive ammuntion
        /// </summary>
        public bool isExplosive = false;

        /// <summary>
        /// A flag for vampiric ammuntion
        /// </summary>
        public bool healsUser = false;

        /// <summary>
        /// Reference for shrapnel
        /// </summary>
        public GameObject secondaryPayload;

        /// <summary>
        /// Audio to be played when the projectile is instantiated
        /// </summary>
        AudioSource source;

        /// <summary>
        /// The current age of the projectile
        /// </summary>
        float age = 0;

        /// <summary>
        /// minimum lifespan for explosive projectiles
        /// </summary>
        float minimumArmingTime = .5f;

        Vector3 velocity = Vector3.zero;


        /// <summary>
        /// Called upon entering the state, sets the velocity in m/s and plays audio
        /// </summary>
        void Start()
        {
            source = GetComponent<AudioSource>();
            if (source != null)
            {
                source.Play();
            }
            velocity = transform.right * speed;

        }
 
        /// <summary>
        /// Called each frame, handles timers for ammunition lifespans and updates velocities
        /// </summary>
        void Update()
        {
            if (age >= lifespan)
                //The projectile has reached its lifespan
            {
                if (isExplosive && secondaryPayload != null)
                    //Detonate now if explosive
                {
                    FireSecondaryPayload();
                }
                Destroy(gameObject); //Remove the object from the scene
            }
            if (isExplosive && secondaryPayload != null && Input.GetButtonUp("Fire1"))
                //Detonation was triggered
            {
                if (age >= minimumArmingTime)
                    //The projectile can explode
                {
                    FireSecondaryPayload();
                    Destroy(gameObject);
                }
                lifespan = minimumArmingTime; //The projectile is too young, detonate at the earliest opportunity
            }

            age += Time.deltaTime; //They grow up so fast
            transform.position += velocity * Time.deltaTime; //They go really fast too
        }

        void OnTriggerEnter(Collider collider)
        {
            //The projectile collided with an object            
            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if (dt != null)
                //Do damage to the object
            {
                dt.TakeDamage(damageAmount); // Apply damage to the thing we hit
                Destroy(gameObject); // Destroy the bullet
                return;                
            }
        }

        /// <summary>
        /// instantiate 9 projectiles in a 270 degree arc from the vector of travel
        /// </summary>
        void FireSecondaryPayload()
        {
            float yaw = transform.eulerAngles.y;
            float spread = 30;
            
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw - spread * 4, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw - spread * 3, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw - spread * 2, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(secondaryPayload, transform.position, transform.rotation);
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw + spread, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw + spread * 2, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw + spread * 3, 0));
            Instantiate(secondaryPayload, transform.position, Quaternion.Euler(0, yaw + spread * 4, 0));
            
        }
    }
}