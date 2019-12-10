using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// Controller for the camera to follow the player
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The player's transform
        /// </summary>
        public Transform lookTarget;

        /// <summary>
        /// Smoothing applied to motion
        /// </summary>
        public float easeMultiplier = 2;

        /// <summary>
        /// The current zoom of the camera
        /// </summary>
        public float zoomValue = 10;

        AudioSource source; // Music

        Camera cam;
        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
            if (source != null)
            {
                source.Play();
            }

            cam = GetComponentInChildren<Camera>();
        }

        // Called after physics
        void FixedUpdate()
        {
            // apply smoothing on the way to lookTarget.position
            if (lookTarget != null)
            {
                transform.position = Vector3.Lerp(transform.position, lookTarget.position, Time.deltaTime * easeMultiplier);
            }

            
            if (cam != null)
            {
                cam.orthographicSize = zoomValue; //adjust zoom to the target
            }

        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {
            zoomValue -= Input.mouseScrollDelta.y;
            zoomValue = Mathf.Clamp(zoomValue, 8, 12);
        }
    }
}