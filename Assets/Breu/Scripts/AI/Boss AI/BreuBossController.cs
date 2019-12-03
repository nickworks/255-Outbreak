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
        public float ChargeTimeLeft;//how long the charge is for the left attack is seconds
        public float ChargeTimeRight;//how long the charge is for the right attack is seconds
        public float ChargeTimeHead;//how long the charge is for the head attack is seconds
        public float IdleTimer = 3;//number of seconds spend idling
        public float ResetTimer = 3;//number of seconds spend reseting

        BreuBossState CurrentState;
        #endregion

        #region Physics Variables
        

        public float DecelerationLeft = 2;
        public float AccelerationLeft = 10;
        public float MovementRangeLeft = 10;

        public float DecelerationRight = 2;
        public float AccelerationRight = 10;
        public float MovementRangeRight = 10;

        public float DecelerationHead = 2;
        public float AccelerationHead = 10;
        public float MovementRangeHead = 10;

        [HideInInspector]
        public Vector3 VelocityRight = Vector3.zero;
        [HideInInspector]
        public Vector3 VelocityLeft = Vector3.zero;
        [HideInInspector]
        public Vector3 VelocityHead = Vector3.zero;

        [HideInInspector]
        public Vector3 StartRight;
        [HideInInspector]
        public Vector3 StartLeft;
        [HideInInspector]
        public Vector3 StartHead;
        #endregion



        void Start()
        {
            ChangeState(new BreuBossIdle());

            StartRight = HandRight.position;
            StartLeft = HandLeft.position;
            StartHead = Head.position;
        }


        void Update()
        {
            MoveParts();

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

        private void MoveParts()
        {
            VelocityHead = Vector3.Lerp(VelocityHead, Vector3.zero, Time.deltaTime * DecelerationHead);
            Head.transform.position += VelocityHead * Time.deltaTime;

            VelocityLeft = Vector3.Lerp(VelocityLeft, Vector3.zero, Time.deltaTime * DecelerationLeft);
            HandLeft.transform.position += VelocityLeft * Time.deltaTime;

            VelocityRight = Vector3.Lerp(VelocityRight, Vector3.zero, Time.deltaTime * DecelerationRight);
            HandRight.transform.position += VelocityRight * Time.deltaTime;
        }
    }
}