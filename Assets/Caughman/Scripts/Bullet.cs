using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Caughman
{
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Speed in meters per second a bullet will fly
        /// </summary>
        public float speed = 10;
        /// <summary>
        /// How long a bullet will be on screen in seconds
        /// </summary>
        public float lifespan = 3;
        /// <summary>
        /// How much damage the Bullet does to a Damage Taker
        /// </summary>
        public float damageAmount = 20;
        /// <summary>
        /// The Object Shooting the Bullet
        /// </summary>
        public Transform bulletShooter;
        /// <summary>
        /// how long bullet has been on screen
        /// </summary>
        float age = 0;
        /// <summary>
        /// Velocity of the Bullet
        /// </summary>
        Vector3 velocity = Vector3.zero;
        

        void Start()
        {
            velocity = transform.right * speed;
        }

        /// <summary>
        /// Moves the Bullets, adds age and checks to see if the Bullet dies from being on screen too long
        /// </summary>
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);

            transform.position += velocity * Time.deltaTime;
        }//End Update
        /// <summary>
        /// What happens when a bullet Collides with another Kinimatic Object
        /// </summary>
        /// <param name="collider"></param>
        void OnTriggerEnter(Collider collider)
        {
            //if the bullet collides with the person who shot it, don't do anything
            if (collider.transform == bulletShooter) return;

            //Checks to see if Collision is made with a Damage Taker Object
            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if (dt != null)
            {
                dt.TakeDamage(damageAmount);//hurt the thing we hit
                Destroy(gameObject);//remove bullet after hit

                gameObject.BroadcastMessage("Hit");

                return;
            }
        }
    }
}
