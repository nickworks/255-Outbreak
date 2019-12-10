using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Bullet class.
    /// Can be red (player) or green (boss).
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Speed that the bullet travels
        /// </summary>
        public float speed = 10;
        /// <summary>
        /// Length of time that bullet stays alive
        /// </summary>
        public float lifespan = 3;
        /// <summary>
        /// Amount of damage dealth
        /// </summary>
        public float damageAmount = 10;
        /// <summary>
        /// Length of time that bullet is alive
        /// </summary>
        public float age = 0;
        /// <summary>
        /// The object that shoots the bullet
        /// </summary>
        public Transform bulletShooter;
        /// <summary>
        /// Moving velocity
        /// </summary>
        Vector3 velocity = Vector3.zero;

        /// <summary>
        /// Called on start.
        /// Set velocity.
        /// Rotate player bullet.
        /// </summary>
        void Start()
        {
            if (gameObject.ToString().Contains("Red"))
                transform.Rotate(90, 0, 90);
            velocity = transform.right * speed;
        }

        /// <summary>
        /// Called every frame.
        /// Move bullet forward.
        /// </summary>
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan)
                Destroy(gameObject);

            transform.position += velocity * Time.deltaTime;
        }

        /// <summary>
        /// Deal damage during collision with object, then destroy bullet
        /// </summary>
        /// <param name="collider"></param>
        void OnTriggerEnter(Collider collider)
        {
            if (collider.transform == bulletShooter)
                return;

            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if (dt != null)
            {
                dt.TakeDamage(damageAmount); // hurt the thing we hit...
                Destroy(gameObject); // remove the bullet
                return;
            }
        }
    }
}