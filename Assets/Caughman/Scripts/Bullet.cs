using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Caughman
{
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Speed in meters per second a bullet will fly
        /// </summary>
        public float speed = 10;
        /// <summary>
        /// How long a bullet will be on screen in seconds
        /// </summary>
        public float lifespan = 3;

        public float damageAmount = 20;
        /// <summary>
        /// how long bullet has been on screen
        /// </summary>
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
        }//End Update

        void OnTriggerEnter(Collider collider)
        {
            DamageTaker dt = collider.GetComponent<DamageTaker>();
            if (dt != null)
            {
                dt.TakeDamage(damageAmount);//hurt the thing we hit
                Destroy(gameObject);//remove bullet after hit
                return;
            }
        }
    }
}
