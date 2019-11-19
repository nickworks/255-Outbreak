using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace andrea
{
    public class BulletController : MonoBehaviour
    {
        public float speed = 10;
        public float lifespan = 3;

        float age = 0;

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            velocity = transform.right * speed;
        }

 
        void Update()
        {
            if (age >= lifespan)
            {
                Destroy(gameObject);
            }
            age += Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }
}