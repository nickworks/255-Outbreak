using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myles
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool useMouseForAiming;
        public float speed = 5;


        CharacterController pawn;
        Camera cam;

        void Start()
        {
            //cam = GameObject.FindObjectOfType<Camera>();
            cam = Camera.main;
            pawn = GetComponent<CharacterController>();
            
        }

        void FixedUpdate()
        {
            Move();
        }


        void Update()
        {
            DetectInputMethod();
            
            if (useMouseForAiming) RotateWithMouse();
            else RotateWithAnalogStick();
        }

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
            if (input.sqrMagnitude > threshold * threshold)
            {
                useMouseForAiming = false;
            }
        }

        private void RotateWithMouse()
        {
            if (cam == null)
            {

                //Debug.LogError("There's no camera to do a raycast from...");
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


        private void RotateWithAnalogStick()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            //print($"horizontal input: {h}  vertical input: {v}");

            Vector3 dir = new Vector3(h, 0, v);

            if (dir.magnitude < .5f) return;

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;

            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;
            Vector3 delta = dir * speed * Time.deltaTime;

            pawn.Move(delta);
        }
    }
}

