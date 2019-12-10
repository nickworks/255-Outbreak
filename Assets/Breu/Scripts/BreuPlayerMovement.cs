using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breu
{
    public class BreuPlayerMovement : MonoBehaviour
    {
        public bool useMouseForAim = true;//if the mouse is to be used to aim

        public float MoveSpeed = 10;//how fast the player can go

        public float Deadzone = .25f;//minimum amount of input needed on joysticks to register as movement
        
        Camera cam;

        public GameObject MainBody;

        BreuDamageTake Status;

        void Start()
        {
            cam = Camera.main;
            Status = MainBody.GetComponent<BreuDamageTake>();
        }


        void Update()
        {
            //if game is not paused, do movement logic
            if (Game.isPaused == false)
            {
                //if status is not null and current health is greater than zero continue with movement logic
                if (Status != null && Status.CurrentHealth > 0)
                {

                    DetectInputMethod();

                    Move();

                    //if not using mouse to aim, use joystick
                    if (!useMouseForAim)
                    {
                        RotateWithAnalog();
                    }
                    //if using mouse to aim, use mouse
                    else
                    {
                        RotateWithMouse();
                    }
                }
            }
        }
        /// <summary>
        /// determines if mouse or joystick should be used for aim, default is joystick
        /// </summary>
        private void DetectInputMethod()
        {
            float mh = Input.GetAxis("Mouse X");
            float mv = Input.GetAxis("Mouse Y");

            if (mh != 0 || mv != 0)
            {
                useMouseForAim = true;

            }

            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector2 input = new Vector2(h, v);
            if (input.sqrMagnitude > Deadzone * Deadzone)
            {
                useMouseForAim = false;
            }
        }

        /// <summary>
        /// rotation logic if using a mouse
        /// </summary>
        private void RotateWithMouse()
        {
            //if camer is null, throw error and return
            if (cam == null)
            {
                Debug.LogError("There is no camera to raycast from");

                return;
            }


            //cast a ray from mouse position to world position
            Plane plane = new Plane(Vector3.up, transform.position);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //rotate player towards mouse position 
            if (plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 delta = mousePos - transform.position;

                float radians = Mathf.Atan2(-delta.z, delta.x);
                float degrees = radians * 180 / Mathf.PI;

                transform.eulerAngles = new Vector3(0, degrees, 0);
            }
        }

        /// <summary>
        /// rotation logic if using a joystick
        /// </summary>
        private void RotateWithAnalog()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");
            //set direction player wants to rotate
            Vector3 dir = new Vector3(h, 0, v);

            //converts radians to degrees
            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI;

            //if dir is not large enough return out of function early
            if (dir.magnitude < .5f)
            {
                return;
            }

            //change euler angles to new direction
            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        /// <summary>
        /// move player based on horizontal and vetical inputs
        /// </summary>
        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;//direction the player wants to move

            transform.position += dir * MoveSpeed * Time.deltaTime;
        }
    }
}