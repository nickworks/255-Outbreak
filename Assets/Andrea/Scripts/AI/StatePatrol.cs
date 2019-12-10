using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrea
{
    /// <summary>
    /// State for patrolling an area after the enemy has been aggroed
    /// </summary>
    public class StatePatrol : EnemyState
    {
        /// <summary>
        /// How fast in m/s the enemy turns towards their destination
        /// </summary>
        public float turnSpeed = 1f;

        float rayLength; // ray for checking for obstacles
        float stoppingDistance = 1f; // The distance until a new destination is needed

        // positional reference for the destination
        Vector3? destination; 
        Vector3 direction; 


        Quaternion desiredRotation; //The rotation towards the destination

        LayerMask layerMask = LayerMask.NameToLayer("Impassable"); //Tag for obstacles

        /// <summary>
        /// Called upon entering the state, sets the ray cast length to the enemyController's pursue distance limit
        /// </summary>
        /// <param name="enemy"></param>
        public override void OnBegin(EnemyController enemy)
        {
            base.OnBegin(enemy);

            rayLength = enemy.pursueDistanceThreshold;            
        }
        /// <summary>
        /// Called every frame, updates transform data
        /// </summary>
        /// <returns></returns>
        public override EnemyState Update()
        {
            if (enemy == null)
            {
                return null; //There is no enemy to control
            }

            if (enemy.attackTarget == null)
            {
                return null; //enemy has nothing it wants to attack
            }
            ///// BEHAVIOR:
            Vector3 disToDestination;

            if (destination.HasValue)
            {
                disToDestination = enemy.transform.position - destination.Value; // The distance to the destination
            }
            else
            {
                disToDestination = Vector3.zero;
            }

            if (!destination.HasValue || disToDestination.sqrMagnitude <= stoppingDistance * stoppingDistance)
                //The enemy has reached their destination and needs a new one
            {
                FindRandomDestination();
            }

            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, desiredRotation, Time.deltaTime * turnSpeed); //Turn towards the destination

            if (isPathAheadBlocked())
                //Forward vector is blocked, just rotate away
            {
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, desiredRotation, .2f);
            }
            else
            {
                //Continue on the path
                enemy.velocity += (disToDestination.normalized * enemy.acceleration * Time.deltaTime);
            }

            while (isPathBlocked())
            {
                //Search for a reachable destination
                FindRandomDestination();            }

            
            void FindRandomDestination() //Selects a random destination
            {
                Vector3 candidatePosition = (enemy.transform.position + enemy.transform.forward * 3) + new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
                destination = new Vector3(candidatePosition.x, enemy.transform.position.y, candidatePosition.z); // Grab a random position

                direction = Vector3.Normalize(destination.Value - enemy.transform.position); //Find its direction
                desiredRotation = Quaternion.LookRotation(direction); //Desire the rotation
            }

            bool isPathAheadBlocked() //Checks the forward vector for obstacles each frame
            {
                Ray ray = new Ray(enemy.transform.position, enemy.transform.forward);
                return Physics.SphereCast(ray, 0.5f, rayLength, layerMask);
            }

            bool isPathBlocked() //Checks the path along the desired rotation for obstacles each frame.
            {
                Ray ray = new Ray(enemy.transform.position, direction);
                return Physics.SphereCast(ray, 0.5f, rayLength, layerMask);
            }

            ///// TRANSITIONS TO OTHER STATES:
            Vector3 disToTarget = enemy.transform.position - enemy.attackTarget.position;
            if (disToTarget.sqrMagnitude < enemy.pursueDistanceThreshold * enemy.pursueDistanceThreshold)
            {
                return new StatePursue();
            }

            //if player is closer than 10m
            //return new StatePursue();

            return null;
        }

    }
}