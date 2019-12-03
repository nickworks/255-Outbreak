using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject target;
        public float ease = 10f;

        // Update is called once per frame
        void Update()
        {

            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * ease);
        }
    }
}