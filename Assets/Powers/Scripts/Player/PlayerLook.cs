using UnityEngine;

namespace Powers
{
    public class PlayerLook : MonoBehaviour
    {
        //used to indicate if using mouse or controller
        public bool useMouseForAiming;
        public Camera cam;

        [HideInInspector]
        public Vector3 lookDirection;

        // Update is called once per frame
        void Update()
        {
            //if game is not paused, get rotation of inputs
            if (!Game.isPaused)
            {
                //depending on player controls, do one or the other
                if (useMouseForAiming) RotateWithMouse();
                else RotateWithAnalogStick();
            }
        }

        private void RotateWithMouse()
        {
            Plane plane = new Plane(Vector3.up, transform.position);

            //cast a ray and use mouse position to get direction
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 vectorToMousePos = mousePos - transform.position;

                float radians = Mathf.Atan2(vectorToMousePos.z, vectorToMousePos.x);
                float degrees = radians * 180 / Mathf.PI;
                lookDirection = new Vector3(90, -degrees + 90, 0);
                transform.eulerAngles = lookDirection;
            }
        }


        private void RotateWithAnalogStick()
        {
            //get values on the right thumbstick
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            //put values into vector
            Vector3 dir = new Vector3(h, 0, v);

            //apply deadzone
            if (dir.magnitude < .5f) return;

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;

            lookDirection = new Vector3(90, degrees - 90, 0);
            transform.eulerAngles = lookDirection;
        }
    }
}

