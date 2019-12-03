using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens { 
    public class Boomerang : MonoBehaviour
    {
        public float lifespan = 3;

        float speed = 35f;
        float age = 0;
        public GameObject model;


        Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            velocity = transform.right * speed;
        }

        // Update is called once per frame
        void Update()
        {
 
            model.transform.RotateAroundLocal(transform.up, 15f * Time.deltaTime);
            velocity += -transform.right * 35f * Time.deltaTime;
            age += Time.deltaTime;
            
            if (age >= lifespan) Destroy(gameObject);


            transform.position += velocity * Time.deltaTime;
        }
    }
}
