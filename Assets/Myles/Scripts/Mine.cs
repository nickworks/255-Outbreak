using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class Mine : MonoBehaviour
    {

        
        public float lifespan = 10;

        float age = 0;
        

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            age += Time.deltaTime;
            if (age >= lifespan) Destroy(gameObject);
            
            
        }
    }
}