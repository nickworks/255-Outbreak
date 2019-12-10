using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BossPunch : MonoBehaviour
    {
        [HideInInspector]
        public bool FinishedPunch = false;//if the atack is complete

        public float PunchSpeed = 10;//speed of punch

        public float Damage = 1;// how much damage can the punch do to the player

        public float PunchDuration;// how long the punch can last

        private float CurrentDuration = 0;//how long the punch has been happening

        Vector3 Velocity = Vector3.zero;

        public GameObject MainBody;

        private Renderer Rend;
        public Material AttackMaterial;
        public Material DefaultMat;

        // Start is called before the first frame update
        void Start()
        {
            Velocity = transform.right;
            if(MainBody!= null)
            {
                Rend = MainBody.GetComponent<Renderer>();
            }
        }

        /// <summary>
        /// Moves the "fist" to simulate a punch
        /// </summary>
        public void Punch()
        {
            if (Rend!= null)
            {
                Rend.material = AttackMaterial;
            }
            transform.position += Velocity * PunchSpeed * Time.deltaTime;
            CurrentDuration += Time.deltaTime;
            if (CurrentDuration >= PunchDuration)
            {
                CurrentDuration = 0;
                FinishedPunch = true;
                if (Rend != null)
                {
                    Rend.material = DefaultMat;
                }
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
                    DT.TakeDamage(Damage);//damages object
                }
            }
        }
    }
}