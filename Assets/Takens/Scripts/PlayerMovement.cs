using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    /// <summary>
    /// Class for handeling the players movement
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {

        /// <summary>
        /// Speed of the player in meters per second
        /// </summary>
        public float speed = 5f;

        /// <summary>
        /// Boolean for wether or not we are using a mouse and keyboard or controller to control the player
        /// </summary>
        public bool useMouseForAiming = true;

        /// <summary>
        /// Reference to the character controller component
        /// </summary>
        CharacterController pawn;

        /// <summary>
        /// Reference to the main camera
        /// </summary>
        Camera cam;


        /// <summary>
        /// This method is called once on startup
        /// </summary>
        void Start()
        {
            cam = Camera.main;
            pawn = GetComponent<CharacterController>();
        }

        /// <summary>
        /// This method can be called multiple times per frame and tries to compensate for different frame rates
        /// </summary>
        void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// This update is called once per frame
        /// </summary>
        void Update()
        {

            DetectInputMethod();
            
            if (!useMouseForAiming) RotateWithAnalogStick();
            else RotateWithMouse();
        }

        /// <summary>
        /// The method is called once per frame and is used to determine the method of control
        /// (mouse/controller)
        /// </summary>
        private void DetectInputMethod()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            if (x != 0 || y != 0)
            {
                useMouseForAiming = true;
            }

            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");
            Vector2 input = new Vector2(h, v);
            float threshold = .25f;
            if (input.sqrMagnitude > threshold * threshold) useMouseForAiming = false;
        }

        /// <summary>
        /// Logic for rotating the player using the mouse
        /// called once per frame if we are using the mouse as the method of control
        /// </summary>
        private void RotateWithMouse(){
            if (cam == null)
            {
                Debug.LogError("There's no camera to do a raycast from!");
                return;
            }
            Plane plane = new Plane(Vector3.up, transform.position);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 vectorToMousePos = mousePos - transform.position;

                float radians = Mathf.Atan2(vectorToMousePos.z, vectorToMousePos.x);
                float degrees = radians * 180 / Mathf.PI;
                transform.eulerAngles = new Vector3(0, -degrees, 0);
            }
        }

        /// <summary>
        /// Logic for rotating the player using a controller
        /// called once per frame if we are using a controller as the method of control
        /// </summary>
        private void RotateWithAnalogStick()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector3 dir = new Vector3(h, 0, v);
            if (dir.magnitude < .3f) return;

            float rads = Mathf.Atan2(v, h);
            float degs = rads * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(0, degs, 0);
        }

        /// <summary>
        /// Logic for moving the player using the Input Axis'
        /// </summary>
        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(h, 0, v).normalized;

            Vector4 delta = dir * speed * Time.fixedDeltaTime;

            pawn.Move(delta);
        }
    }
}