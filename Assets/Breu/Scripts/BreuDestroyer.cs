using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuDestroyer : MonoBehaviour
    {
        public float Lifespan = 3;//how long the objects can live

        // reduces lifespan, if lifespan is less than or equals zero, destory gameobject
        void Update()
        {
            Lifespan -= Time.deltaTime;
            if (Lifespan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}