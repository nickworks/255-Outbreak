using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifespan = 3;

        float age = 0;


        Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            velocity = transform.right * speed;
        }

        // Update is called once per frame
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);


            transform.position += velocity * Time.deltaTime;
        }
    }
}