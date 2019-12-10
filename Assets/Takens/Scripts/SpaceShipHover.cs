using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens {
    /// <summary>
    /// Class used to make the spaceship look like its hovering,
    /// as well as look at the player
    /// </summary>
    public class SpaceShipHover : MonoBehaviour
    {
        /// <summary>
        /// Vertical distance the hovering goes up and down
        /// </summary>
        public float HoverAmmount = 3f;

        /// <summary>
        /// Reference to what this object will look at, most likely the player
        /// </summary>
        public GameObject LookAtTarget;

        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {
            if (Game.isPaused) return;
            Vector3 newPos = transform.position;
            newPos.y += (Mathf.Cos(Time.time*1.2f) * HoverAmmount);
            transform.position = newPos;


            transform.LookAt(LookAtTarget.transform, Vector3.up);

    }
    }
}