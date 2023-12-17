using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupWander : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float damping = 0.1f; // Damping factor
    public Transform[] waypoints; // Array of waypoints in the pathfinding area

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Check if there are waypoints
        if (waypoints.Length > 0)
        {
            // Calculate direction towards the current waypoint
            Vector2 directionToWaypoint = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            // Apply damping to smooth out movement
            directionToWaypoint = Vector2.Lerp(Vector2.zero, directionToWaypoint, 1 - damping);

            // Move the enemy in the direction of the current waypoint
            transform.Translate(directionToWaypoint * wanderSpeed * Time.deltaTime);

            // Check if reached the current waypoint
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }
}

