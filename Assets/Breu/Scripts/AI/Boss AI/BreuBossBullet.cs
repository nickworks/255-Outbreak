using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Breu
{
    public class BreuBossBullet : MonoBehaviour
    {
        public float speed = 20;

        public float lifeSpan = 3;

        public float Damage = 1;

        public bool DisappearsOnHit = true;

        private float age = 0;

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            velocity = transform.right;
        }


        void Update()
        {
            transform.position += velocity * speed * Time.deltaTime;
            age += Time.deltaTime;
            if (age >= lifeSpan)
            {
                Destroy(transform.parent.gameObject);//destroys bullet group at end of life
            }
        }

        void OnTriggerEnter(Collider col)
        {
            BreuDamageTake DT = col.GetComponent<BreuDamageTake>();

            if (col.gameObject.tag == "BreuPlayer")
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