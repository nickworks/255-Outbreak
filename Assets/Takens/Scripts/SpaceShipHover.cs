using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens {
    public class SpaceShipHover : MonoBehaviour
    {
        public float HoverAmmount = 3f;
        public GameObject LookAtTarget;

        // Update is called once per frame
        void Update()
        {
            Vector3 newPos = transform.position;
            newPos.y += (Mathf.Cos(Time.time*1.2f) * HoverAmmount);
            transform.position = newPos;

            transform.LookAt(LookAtTarget.transform, Vector3.up);

    }
    }
}