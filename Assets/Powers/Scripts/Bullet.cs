using UnityEngine;

namespace Powers
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10;
        public float lifespan = 3;

        float age = 0;

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            velocity = transform.up * speed;
        }

        void Update()
        {
            //add age to bullet. if bullet gets too old, destroy it
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);

            //add velocity
            transform.position += velocity * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider collider)
        {
            //if it collides with something other than the player, destroy the bullet
            if (collider.tag == "GameController" || collider.tag == "Finish") Destroy(gameObject);
        }
    }
}

