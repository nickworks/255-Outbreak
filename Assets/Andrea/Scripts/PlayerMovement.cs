using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace andrea
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool useMouseForAiming = true;
        public float speed = 5;

        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GameObject.FindObjectOfType<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();

            if (!useMouseForAiming)
            {
                RotateWithAnalogueStick();
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
                Debug.LogError("There's no camera available to raycast from.");
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

        private void RotateWithAnalogueStick()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector3 dir = new Vector3(h, 0, v);
            if (dir.magnitude < .5)
            {
                return;
            }

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;

            transform.position += dir * speed * Time.deltaTime;
        }
    }
}