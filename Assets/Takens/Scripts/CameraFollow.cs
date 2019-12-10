using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Class to get the cameras parent to follow the player
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// Target to follow
        /// (the player)
        /// </summary>
        public GameObject target;

        /// <summary>
        /// Ammount of smoothing in camera follow motion
        /// </summary>
        public float ease = 10f;

       /// <summary>
       /// This method is called once per frame
       /// </summary>
        void Update()
        {

            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * ease);
        }
    }
}