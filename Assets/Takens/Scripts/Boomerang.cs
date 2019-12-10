using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens { 
    /// <summary>
    /// Class for physics of the boomerang projectile
    /// </summary>
    public class Boomerang : MonoBehaviour
    {
        /// <summary>
        /// Lifespan of the boomerang in seconds
        /// </summary>
        public float lifespan = 3;

        /// <summary>
        /// Initial speed of the boomerang in seconds
        /// </summary>
        float speed = 35f;

        /// <summary>
        /// Current age of the boomerange
        /// </summary>
        float age = 0;

        /// <summary>
        /// Refernce to the model, used to spin it
        /// </summary>
        public GameObject model;

        /// <summary>
        /// Velocity of the boomerang in meters per second
        /// </summary>
        Vector3 velocity = Vector3.zero;

        /// <summary>
        /// This method is called once on start up
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
 
            model.transform.RotateAroundLocal(transform.up, 15f * Time.deltaTime);

            //force that makes the boomerance change direction
            velocity += -transform.right * 35f * Time.deltaTime;
            age += Time.deltaTime;
            
            if (age >= lifespan) Destroy(gameObject);


            transform.position += velocity * Time.deltaTime;
        }
    }
}
