using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {
    public class PlayerMovement : MonoBehaviour {

        public bool useMouseForAiming = true; // determines if player will be using mouse to aim
        public float speed = 5; // speed of player movement

        CharacterController pawn; // summons camera controller component
        Camera cam; // summons camera

        // Establishes the camera
        // Start is called before the first frame update
        void Start()
        {
            cam = GameObject.FindObjectOfType<Camera>();
            cam = Camera.main;
            pawn = GetComponent<CharacterController>();
        }
        // Moves the camera appropriately on a FixedUpdate
        void FixedUpdate()
        {
            Move();
        }

        // Checks for mouse/analog location per frame
        // Update is called once per frame
        void Update()
        {
            DetectInputMethod();

            if (!useMouseForAiming) RotateWithAnalogStick();
            if (useMouseForAiming) RotateWithMouse();

        }
        //Detects and interprets input on x/y axis
        private void DetectInputMethod()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            // Uses axis when the x & y aren't 0
            if (x != 0 || y != 0)
            {
                useMouseForAiming = true;
            }


            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            // Checks to see if a threshold for input is being pushed
            Vector2 input = new Vector2(h, v);
            float threshold = .25f;
            if (input.sqrMagnitude > threshold * threshold)
            {
                // switch to controller aiming...
                useMouseForAiming = false;
            }
        }

        // Rotate character with mouse cursor location
        private void RotateWithMouse()
        {
            if(cam == null)
            {

                Debug.LogError("There's no camera to do a raycast from...");
                return;
            }

            Plane plane = new Plane(Vector3.up, transform.position);

            // Rotates based on radians/degree conversion to make the mouse line up with shooting pos.
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
        // Rotates based on radians/degree conversion to make the analog line up with shooting pos.

        private void RotateWithAnalogStick()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            print($"horizontal input: {h}   vertical input: {v}");

            Vector3 dir = new Vector3(h, 0, v);

            if (dir.magnitude < .5f) return;

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;

            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        // Moves the player
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
