using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Caughman
{
    public class CameraController : MonoBehaviour
    {
        public float easeMultiplyer = 10;

        public Transform lookTarget;

        public float zoomValue = 10;
        Camera cam;

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }


        void Update()
        {
            //zoomValue -= Input.mouseScrollDelta.y * 2;
        }

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
