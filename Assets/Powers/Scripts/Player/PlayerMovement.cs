using UnityEngine;

namespace Powers
{
    public class PlayerMovement : MonoBehaviour
    {

        public float normalSpeed;
        public float shiftSpeed;
        private Vector3 moveDirection;
        private CharacterController charController;
        [HideInInspector]
        public float currentSpeed;

        void Start()
        {
            //looks at player object to find char controller
            charController = gameObject.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!Game.isPaused)
            {
                #region Get Movement Vector
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")); //grab the input on the horizontal and vertical axis
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1); //clamp the magnitude

                //multiply the move vector by the speed
                if (Input.GetButton("Fire3"))
                {
                    moveDirection.x *= shiftSpeed;
                    moveDirection.z *= shiftSpeed;
                }
                else
                {
                    moveDirection.x *= normalSpeed;
                    moveDirection.z *= normalSpeed;
                }
                #endregion

                //apply the movement vector
                charController.Move(moveDirection * Time.deltaTime);

                currentSpeed = charController.velocity.magnitude;
            }

        }
    }
}