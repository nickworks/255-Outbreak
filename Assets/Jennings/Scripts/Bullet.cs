using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {
    public class Bullet : MonoBehaviour {
        public float speed = 10; // The speed at which the bullet moves
        public float lifespan = 3; // How long the bullet should live
        public float damageAmount = 10; // The amount of damage the bullet does

        float age = 0; // determines the given age something should be able to hit prior to death

        Vector3 velocity = Vector3.zero;

        // Calcs bullet velocity
        // Start is called before the first frame update
        void Start()
        {
            velocity = transform.right * speed;
        }

        // Checks for bullet age so it can destroy it if it gets too old
        // Update is called once per frame
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);
            transform.position += velocity * Time.deltaTime;
        }

        // Checks to see if bullet hits a collider, destroying it
        void OnTriggerEnter(Collider collider)
        {
            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if(dt != null)
            {
                dt.TakeDamage(damageAmount); // hurt the thing we hit
                Destroy(gameObject); // remove bullet from game
                return;
            }


            // Do other things...

        }

    }
}