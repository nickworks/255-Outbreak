using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class CameraController : MonoBehaviour
    {
        public float easeMultiplier = 10;
        public Transform lookTarget;
        Camera cam;

        public float zoomValue = 10;
        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            zoomValue -= Input.mouseScrollDelta.y * 2;

            zoomValue = Mathf.Clamp(zoomValue, 5, 50);
        }


        void FixedUpdate()
        {
            if (lookTarget != null)
            {

                transform.position = Vector3.Lerp(transform.position, lookTarget.position, Time.deltaTime * easeMultiplier);
            }
            if (cam != null)
            {
                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, -zoomValue), Time.deltaTime * easeMultiplier);
            }
        }
    }
}
