using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Takens {
    /// <summary>
    /// Class added to bullets to be referenced during collisions for how much damage is dealt
    /// </summary>
    public class Damage : MonoBehaviour
    {
        /// <summary>
        /// Is this a bullet fired at the enemy from the player? (true)
        /// Or is this a bullet fired at the player from the enemy? (false)
        /// </summary>
        public bool friendly = true;

        /// <summary>
        /// How much damage this bullet will do during a collision
        /// </summary>
        public float damage = 5f;

    }
}