using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{

    public class PlayerMovement : MonoBehaviour
    {
        public bool useMouseForAiming = false; //switch between mouse/keyboard and controller
        public float speed = 5; // speed of player, set in inspector
        Camera cam; // camera
        CharacterController pawn; // character controller

        void Start()
        {
            // cam = GameObject.FindObjectOfType<Camera>();
            cam = Camera.main;
            pawn = GetComponent<CharacterController>();
        }
        void FixedUpdate()
        {
            Move();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.isPaused) return;

            DetectInputMethod(); // checks for new inputs to auto switch which input is being used.

            if (!useMouseForAiming) RotateWithAnalongStick(); //switch to controller
            if (useMouseForAiming) RotateWithMouse(); // switch to mouse
        }

        private void DetectInputMethod() //checks for new input to auto switch which input is being used.
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            if (x != 0 || y != 0)
            {
                useMouseForAiming = true;
            }

            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            float threshold = .25f;
            Vector2 input = new Vector2(h, v);
            if (input.sqrMagnitude > threshold * threshold)
            {
                //switch to controller aiming...
                useMouseForAiming = false;
            }
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal"); //keyboard movement
            float v = Input.GetAxisRaw("Vertical"); //keyboard movement
            if (Game.isPaused == false)
            {
                Vector3 dir = new Vector3(h, 0, v).normalized;
                Vector3 delta = dir * speed * Time.fixedDeltaTime;

                pawn.Move(delta);
            }
        }

        private void RotateWithAnalongStick()
        {
            float h = Input.GetAxis("Horizontal2"); // joystick movement
            float v = Input.GetAxis("Vertical2"); // joystick movement

            // print($"Horiztonal input: {h} vertical input: {v}"); used for early testing of axis input

            Vector3 dir = new Vector3(h, 0, v);

            if (dir.magnitude < .5f) return;

            float rads = Mathf.Atan2(v, h);
            float degs = rads * 180 / Mathf.PI;

            transform.eulerAngles = new Vector3(0, degs, 0);
        }

        private void RotateWithMouse() // mouse movement
        {
            if (cam == null)
            {
                Debug.LogError("Theres no camera to do a raycast from...");
                return;
            }

            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 vectorToMousePos = mousePos - transform.position;

                float radians = Mathf.Atan2(vectorToMousePos.z, vectorToMousePos.x);
                float degrees = radians * 180 / Mathf.PI;
                transform.eulerAngles = new Vector3(0, -degrees, 0);
            }

        }

    }
}

