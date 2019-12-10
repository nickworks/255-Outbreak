using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuDamageTake : MonoBehaviour
    {
        public float MaxHealth = 100;
        public float CurrentHealth;

        public bool IsPlayer = false;
        public bool IsBoss = false;

        public bool IsDamaged = false;
        public float DamageTimer = 3;//how long the enemy changes color when damaged(Public)
        private float MaxDamgeTimer;//The max value of DamageTimer


        public GameObject MainBody;//the object that will change color
        public Color StartColor;//the default color
        public Color DamageColor;//the color that will flash when damge is take
        private Renderer Rend;//the render 

        private float EndWaitTimer = 3;
        
        void Start()
        {
            CurrentHealth = MaxHealth;
            MaxDamgeTimer = DamageTimer;
            EndWaitTimer = 5;
            if (MainBody != null)//find the renderer on MainBody
            {
                Rend = MainBody.GetComponent<Renderer>();
                if (Rend != null)
                {
                    Rend.material.SetColor("_Color", StartColor);
                }
            }
        }
        
        void Update()
        {
                if (IsDamaged == true)//changes color of MainBody when hit, then reverts it back
                {
                    //Debug.Log("Damage Taken");//for testing, comment out
                    DamageTimer -= Time.deltaTime;
                    if (Rend != null)//set color to damagecolor
                    {
                        Rend.material.SetColor("_Color", DamageColor);
                    }
                    if (DamageTimer <= 0)
                    {
                        IsDamaged = false;
                        if (Rend != null)//set color to startcolor
                        {
                            Rend.material.SetColor("_Color", StartColor);
                        }
                    }
                }

                if (CurrentHealth <= 0)//reduces end wait time when health is less than or equal to zero, then call OnDeath()
                {

                    EndWaitTimer -= Time.deltaTime;
                    OnDeath();
                }
            
        }

        public void TakeDamage(float Damage)
        {
            CurrentHealth -= Damage;//reduce health by given variable "damage"
            if (CurrentHealth <= 0)//if current health less than zero, call OnDeath()
            {
                OnDeath();
            }

            IsDamaged = true;
            DamageTimer = MaxDamgeTimer;
        }


        //when CurrentHealth reaches or is below zero
        void OnDeath()
        {
            //Debug.Log("enter death");
            Rend.enabled = false;
            //Debug.Log("rend disabled");
            if(IsPlayer == true && EndWaitTimer <= 0)//if the player has died and the end wait timer is finished, GameOver()
            {
                Game.GameOver();
                //Debug.Log("lose");
            }
            if (IsBoss == true && EndWaitTimer <= 0)//if the boss has died and the end wait timer is finished, GoToNextLevel();
            {
                Game.GotoNextLevel();
                //Debug.Log("win");
            }
        }
    }
}