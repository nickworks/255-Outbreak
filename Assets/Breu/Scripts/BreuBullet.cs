using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuBullet : MonoBehaviour
    {
        public float speed = 20;//speed of bullet

        public float lifeSpan = 3;//how long a bullet can last

        public float Damage = 1;//how much damage a bullet does

        public bool DisappearsOnHit = true;//if thje bullet passes through other objects

        private float age = 0;//how long the bullet has existed

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            velocity = transform.right;
        }

        /// <summary>
        /// Moves bullet then deletes if if age is great than/equal to life span
        /// </summary>
        void Update()
        {
            transform.position += velocity * speed * Time.deltaTime;
            age += Time.deltaTime;
            if (age >= lifeSpan)
            {
                Destroy(transform.parent.gameObject);//destroys bullet group at end of life
            }
        }
        /// <summary>
        /// does damage to actors not tagged "BreuPlayer" when colliders enter eachother
        /// </summary>
        void OnTriggerEnter (Collider col)
        {
            BreuDamageTake DT = col.GetComponent<BreuDamageTake>();

            if (col.gameObject.tag != "BreuPlayer")
            {
                if (DT != null)
                {
                    DT.TakeDamage(Damage);//damages object


                }

                if (DisappearsOnHit == true)
                {
                    Destroy(gameObject);//destorys bullet if it should disappear on hit
                }
            }
        }


    }
}