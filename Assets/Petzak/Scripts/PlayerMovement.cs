using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Controls movement of player.
    /// Allows mouse and controller input.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// Use mouse for aiming or not
        /// </summary>
        public bool useMouseForAiming = true;
        /// <summary>
        /// Speed that the player moves
        /// </summary>
        public float speed = 5;

        /// <summary>
        /// Character controller
        /// </summary>
        CharacterController pawn;
        /// <summary>
        /// Camera
        /// </summary>
        Camera cam;

        /// <summary>
        /// Called on start
        /// </summary>
        void Start()
        {
            cam = Camera.main;
            pawn = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Move player
        /// </summary>
        void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// Detect input method and rotate
        /// </summary>
        void Update()
        {
            DetectInputMethod();

            if (useMouseForAiming)
                RotateWithMouse();
            else
                RotateWithAnalogStick();
        }

        /// <summary>
        /// Determines which input method is being used
        /// </summary>
        private void DetectInputMethod()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            if (x != 0 || y != 0)
                useMouseForAiming = true;

            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector2 input = new Vector2(h, v);
            float threshold = .25f;
            if (input.sqrMagnitude > threshold * threshold)
                useMouseForAiming = false;
        }

        /// <summary>
        /// Rotate towards mouse position
        /// </summary>
        private void RotateWithMouse()
        {
            if (cam == null)
            {
                Debug.LogError("There's no camera to do a raycast from...");
                return;
            }

            Plane plane = new Plane(Vector3.up, transform.position);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float dis))
            {
                Vector3 mousePos = ray.GetPoint(dis);

                Vector3 vectorToMousePos = mousePos - transform.position;

                float radians = Mathf.Atan2(vectorToMousePos.z, vectorToMousePos.x);
                float degrees = radians * 180 / Mathf.PI - 90;
                transform.eulerAngles = new Vector3(0, -degrees, 0);
            }
        }

        /// <summary>
        /// Rotate with analog stick
        /// </summary>
        private void RotateWithAnalogStick()
        {
            float h = Input.GetAxis("Horizontal2");
            float v = Input.GetAxis("Vertical2");

            Vector3 dir = new Vector3(h, 0, v);
            if (dir.magnitude < .5f) return;

            float radians = Mathf.Atan2(v, h);
            float degrees = radians * 180 / Mathf.PI - 90;

            transform.eulerAngles = new Vector3(0, degrees, 0);
        }

        /// <summary>
        /// Move player
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