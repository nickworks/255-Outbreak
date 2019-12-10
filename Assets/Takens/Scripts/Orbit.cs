using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Class for the behavior of the orbiting bullet object
    /// </summary>
    public class Orbit : MonoBehaviour
    {
        /// <summary>
        /// The target to orbit
        /// </summary>
        public GameObject target;

        /// <summary>
        /// The speed at which it orbits in meters per second
        /// </summary>
        public float speed = 10f;

        /// <summary>
        /// The lifespan of the bullet in meters per second
        /// </summary>
        public float lifespan = 400;

        /// <summary>
        /// The current age of the bullet
        /// </summary>
        float age = 0;

        /// <summary>
        /// Velocity of the bullet in meters per second
        /// </summary>
        Vector3 velocity = Vector3.zero;

        /// <summary>
        /// This method is called once on start up
        /// </summary>
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player");
            velocity = transform.right * speed;
        }

       /// <summary>
       /// This method is called once per frame
       /// </summary>
        void Update()
        {
            Vector3 accel =  (target.transform.position - transform.position).normalized;

            velocity += (accel*2);//Accelerate towards the player, Also increase the acceleration towards the player so it orbits closer

            velocity *= .99f;//slowly pull the bullet in towards the player

            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);


            transform.position += velocity * Time.deltaTime;
        }
    }
}