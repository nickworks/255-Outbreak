using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Breu
{
    public class BreuBossBullet : MonoBehaviour
    {
        public float speed = 20;//speed of bullet

        public float lifeSpan = 3;//number of seconds bullet can live

        public float Damage = 1;//amount of damage the bullet does

        public bool DisappearsOnHit = true;//if the bullet should pass through others

        private float age = 0;//how long the bullet has existed

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            velocity = transform.right;
        }

        /// <summary>
        /// set velocity of bullet then deletes parent once age is greater than lifespan
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
        /// does damage to actors tagged "BreuPlayer" when colliders enter eachother
        /// </summary>
        void OnTriggerEnter(Collider col)
        {
            BreuDamageTake DT = col.GetComponent<BreuDamageTake>();

            if (col.gameObject.tag == "BreuPlayer")
            {
                if (DT != null)
                {
                    if (DT.CurrentHealth > 0)
                    {
                        DT.TakeDamage(Damage);//damages object


                        if (DisappearsOnHit == true)
                        {
                            Destroy(gameObject);//destorys bullet if it should disappear on hit
                        }
                    }

                }
            }
        }
    }
}