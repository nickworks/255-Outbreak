using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuPlayerMovement : MonoBehaviour
    {
        public bool useMouseForAim = true;

        public float MoveSpeed = 10;
        
        Camera cam;

        void Start()
        {
            cam = Camera.main;
        }


        void Update()
        {
            Move();
            if (!useMouseForAim)
            {
                RotateWithAnalog();
            }
            else
            {
                RotateWithMouse();
            }



        }

        private void RotateWithMouse()
        {
            if (cam == null)
            {
                Debug.LogError("There is no camera to raycast from");

                return;
            }

            Plane plane = new Plane(Vector3.up, transform.position);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 delta = mousePos - transform.position;

                float radians = Mathf.Atan2(-delta.z, delta.x);
                float degrees = radians * 180 / Mathf.PI;

                transform.eulerAngles = new Vector3(0, degrees, 0);
            }
        }

        private void RotateWithAnalog()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector3 dir = new Vector3(h, 0, v);

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;
            if (dir.magnitude < .5f)
            {
                return;
            }

            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;//direction thenplay wants to move

            transform.position += dir * MoveSpeed * Time.deltaTime;
        }
    }
}