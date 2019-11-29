using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu {
    public class BreuBossController : MonoBehaviour
    {
        public Transform HandLeft;
        public Transform HandRight;
        public Transform Head;


        #region State Variables
        public Transform Target;
        public float ChargeTimeLeft;
        public float ChargeTimeRight;
        public float ChargeTimeHead;

        BreuBossState CurrentState;
        #endregion

        #region Physics Variables
        public float IdleTimer = 3;//number of second spend idling

        public float DecelerationLeft = 2;
        public float DecelerationRight = 2;
        public float DecelerationHead = 2;

        public float AccelerationLeft = 2;
        public float AccelerationRight = 2;
        public float AccelerationHead = 2;

        [HideInInspector]
        public Vector3 VelocityRight = Vector3.zero;
        [HideInInspector]
        public Vector3 VelocityLeft = Vector3.zero;
        [HideInInspector]
        public Vector3 VelocityHead = Vector3.zero;
#endregion


                
        void Start()
        {
            ChangeState(new BreuBossIdle());
        }


        void Update()
        {
            BreuBossState newSate = CurrentState.Update();

            ChangeState(newSate);

        }

        /// <summary>
        /// checks if both new and current states are not null, then changes current state to new state
        /// </summary>
        /// <param name="NewState">the state the boss is going to change to</param>
        private void ChangeState(BreuBossState NewState)
        {
            if (NewState != null)
            {
                if (CurrentState != null)
                {
                    CurrentState.OnEnd();
                }
                CurrentState = NewState;
                CurrentState.OnBegin(this);
            }
        }
    }
}