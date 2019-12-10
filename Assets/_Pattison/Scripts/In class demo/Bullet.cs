using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class Bullet : MonoBehaviour
    {
        public float speed = 10;
        public float lifespan = 3;
        public float damageAmount = 10;

        public Transform bulletShooter;

        float age = 0;

        Vector3 velocity = Vector3.zero;

        void Start() {
            velocity = transform.right * speed;
        }

        void Update() {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);

            transform.position += velocity * Time.deltaTime;
        }

        void OnTriggerEnter(Collider collider) {


            if (collider.transform == bulletShooter) return;

            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if(dt != null) {
                dt.TakeDamage(damageAmount); // hurt the thing we hit...
                Destroy(gameObject); // remove the bullet
                return;
            }



            //// do other things...
        }

    }
}