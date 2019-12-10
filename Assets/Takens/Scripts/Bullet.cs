using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Classe to make bullets move in a straight line
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Speed of the bullet in meters per second
        /// </summary>
        public float speed = 10f;

        /// <summary>
        /// Lifespan of bullets before they are destroyed in seconds
        /// </summary>
        public float lifespan = 3;

        /// <summary>
        /// Current age of the bullet in seconds
        /// </summary>
        float age = 0;

        /// <summary>
        /// Velocity of the bullet in meters per second
        /// </summary>
        Vector3 velocity = Vector3.zero;

        /// <summary>
        /// This method is called once before the first frame
        /// </summary>
        void Start()
        {
            velocity = transform.right * speed;
        }

       /// <summary>
       /// This method is called once per frame
       /// </summary>
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);


            transform.position += velocity * Time.deltaTime;
        }
    }
}