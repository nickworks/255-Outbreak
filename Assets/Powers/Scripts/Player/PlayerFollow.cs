using UnityEngine;

namespace Powers
{
    public class PlayerFollow : MonoBehaviour
    {
        //object to follow
        public GameObject objectFollow;
        public float followSpeed;

        //offset to follow
        public Vector3 offset;

        private Vector3 objectPosition;
        private Vector3 currentVelocity;

        private void Start()
        {
            transform.position = new Vector3(objectFollow.transform.position.x + offset.x, objectFollow.transform.position.y + offset.y, objectFollow.transform.position.z + offset.z);
            objectPosition = new Vector3(objectFollow.transform.position.x, objectFollow.transform.position.y, objectFollow.transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            objectPosition = Vector3.SmoothDamp(objectPosition, objectFollow.transform.position, ref currentVelocity, followSpeed, 99f, Time.deltaTime);

            gameObject.transform.position = new Vector3(objectPosition.x + offset.x, objectPosition.y + offset.y, objectPosition.z + offset.z);
        }
    }

}
