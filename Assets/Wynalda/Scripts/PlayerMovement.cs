using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{

    public class PlayerMovement : MonoBehaviour
    {
        public bool useMouseForAiming = false;
        public float speed = 5;
        Camera cam;

        void Start()
        {
            // cam = GameObject.FindObjectOfType<Camera>();
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.isPaused) return;

            Move();
            if (!useMouseForAiming) RotateWithAnalongStick();
            if (useMouseForAiming) RotateWithMouse();
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal"); //keyboard movement
            float v = Input.GetAxisRaw("Vertical"); //keyboard movement

            Vector3 dir = new Vector3(h, 0, v).normalized;
            transform.position += dir * speed * Time.deltaTime;
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

        private void RotateWithMouse()
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

