using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BossPunch : MonoBehaviour
    {
        [HideInInspector]
        public bool FinishedPunch = false;

        public float PunchSpeed = 10;

        public float Damage = 1;

        public float PunchDuration;

        private float CurrentDuration = 0;

        Vector3 Velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            Velocity = transform.right;
        }
        
        public void Punch()
        {

            transform.position += Velocity * PunchSpeed * Time.deltaTime;
            CurrentDuration += Time.deltaTime;
            if (CurrentDuration >= PunchDuration)
            {
                CurrentDuration = 0;
                FinishedPunch = true;
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
            }
        }
    }
}