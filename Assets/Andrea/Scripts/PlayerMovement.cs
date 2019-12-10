using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// Contains all the logic for moving the player
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {   
        /// <summary>
        /// Flag to use mouse aim mode
        /// </summary>
        public bool useMouseForAiming = true; //Deprecated
        
        /// <summary>
        /// The speed in m/s of the player
        /// </summary>
        public float speed = 5;

        /// <summary>
        /// Reference to the CharacterController component
        /// </summary>
        CharacterController pawn;

        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GameObject.FindObjectOfType<Camera>();
            pawn = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Called after physics
        /// </summary>
        void FixedUpdate()
        {
            Move();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.isPaused)
                return;

            DetectInputMethod();

            if (!useMouseForAiming)
            {
                RotateWithAnalogueStick();
            }
            else
            {
                RotateWithMouse();
            }
        }

        /// <summary>
        /// Determines input method
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
            float threshold = .25f; //deadzone
            if (input.sqrMagnitude > threshold * threshold)
            {
                useMouseForAiming = false;
            }
        }

        /// <summary>
        /// Used when aiming with the mouse each frame
        /// </summary>
        private void RotateWithMouse()
        {
            if (cam == null)
            {
                Debug.LogError("There's no camera available to raycast from.");
                return;
            }

            Plane plane = new Plane(Vector3.up, transform.position); //A plane to cast rays on

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

        /// <summary>
        /// Used when aiming with the controller each frame
        /// </summary>
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

        /// <summary>
        /// Shared input for keyboard and controller
        /// </summary>
        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;

            Vector3 delta =  dir * speed * Time.fixedDeltaTime;

            pawn.Move(delta);
        }
    }
}