using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// Ease in Value in meters
        /// </summary>
        public float easeMultiplyer = 10;
        /// <summary>
        /// Area the camera will be looking at
        /// </summary>
        public Transform lookTarget;
        /// <summary>
        /// How far in meters the camera will zoom in on the lookTarget
        /// </summary>
        public float zoomValue = 10;
        /// <summary>
        /// Reference to the Camera Object
        /// </summary>
        Camera cam;

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }


        void Update()
        {
            //zoomValue -= Input.mouseScrollDelta.y * 2;
        }
        /// <summary>
        /// Camera follows look Position with an ease in ease out from camera sway
        /// </summary>
        void FixedUpdate()
        {
            if (lookTarget != null)
            {
                //set Position to: 5% of the distance from where it is to where look target is
                transform.position = Vector3.Lerp(transform.position, lookTarget.position, Time.deltaTime * easeMultiplyer);
            }

            /*
            if(cam != null)
            {
                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, zoomValue, 0), Time.deltaTime * easeMultiplyer);
            }*/
        }//End FixedUpdate
    }
}
