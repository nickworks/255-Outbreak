using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10; //speed of bullet
        public float lifespan = 7; //lifespan of bullet
        public float damageAmount = 10; //amount of damage bullet does, set in inspector
        float age = 0; //age alive to remove

        public Transform bulletShooter; //object that shot the bullet

        Vector3 velocity = Vector3.zero;
       
        void Start()
        {
            velocity = transform.right * speed;
        }

        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);
            transform.position += velocity * Time.deltaTime;
        }

        void OnTriggerEnter(Collider collider)//collision!
        {
            if (collider.transform == bulletShooter) return;

            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if(dt != null)
            {
                dt.TakeDamage(damageAmount); // hurt the thing we hit
                Destroy(gameObject); // remove bullet when it hits something
                return;
            }


            /// do other things?

        }


    }
}
