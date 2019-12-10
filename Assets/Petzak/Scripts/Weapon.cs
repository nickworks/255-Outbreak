using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Petzak
{
    /// <summary>
    /// Object picked up by player to gain triple (purple) or rapid (blue) shot.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        /// <summary>
        /// Time for object to respawn
        /// </summary>
        float timeToRespawn = 0;
        /// <summary>
        /// Rapid shot color
        /// </summary>
        public Material blue;
        /// <summary>
        /// Triple shot color
        /// </summary>
        public Material purple;
        /// <summary>
        /// Mesh renderer component
        /// </summary>
        public MeshRenderer mesh;
        /// <summary>
        /// Collider component
        /// </summary>
        public CapsuleCollider col;
        /// <summary>
        /// Should respawn
        /// </summary>
        bool respawn = false;
        /// <summary>
        /// Alternate between triple and rapid
        /// </summary>
        bool tripleIsNext = false;

        /// <summary>
        /// Called on start.
        /// </summary>
        void Start()
        {
            mesh = GetComponent<MeshRenderer>();
            col = GetComponent<CapsuleCollider>();
            mesh.material = blue;
        }

        /// <summary>
        /// Called every frame.
        /// Re-enables weapon and sets to random location.
        /// </summary>
        void Update()
        {
            timeToRespawn--;

            if (timeToRespawn <= 0)
            {
                mesh.enabled = col.enabled = true;
                if (respawn)
                {
                    respawn = false;
                    SetRandomLocation();
                }
            }
        }

        /// <summary>
        /// Move weapon to random location after spawning
        /// </summary>
        void SetRandomLocation()
        {
            System.Random r = new System.Random();
            float x = r.Next (-12, 12);
            float z = r.Next(-12, 12);
            gameObject.transform.position = new Vector3(x, .17f, z);
        }

        /// <summary>
        /// Called when object collides with weapon.
        /// Disables object, then updates current weapon and hud.
        /// </summary>
        /// <param name="collider"></param>
        void OnTriggerEnter(Collider collider)
        {
            timeToRespawn = 5 * 60; // every 5 seconds
            mesh.enabled = col.enabled = false;
            respawn = true;

            if (collider.gameObject.ToString().Contains("Spider"))
                return;

            if (tripleIsNext)
            {
                mesh.material = blue;
                tripleIsNext = false;
                HUD.instance.UpdateWeapon(PlayerShooting.WeaponType.TripleShot);
            }
            else
            {
                mesh.material = purple;
                tripleIsNext = true;
                HUD.instance.UpdateWeapon(PlayerShooting.WeaponType.AutoRifle);
            }
        }
    }
}