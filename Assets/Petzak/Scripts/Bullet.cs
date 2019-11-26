using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10;
        public float lifeSpan = 3;
        public float age = 0;
        private Vector3 velocity = Vector3.zero;

        void Start() {
            velocity = transform.right * speed;
        }

        void Update() {
            age += Time.deltaTime;
            if (age >= lifeSpan)
                Destroy(gameObject);
            transform.position += velocity * Time.deltaTime;
        }
    }
}