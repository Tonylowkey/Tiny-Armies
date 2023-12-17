using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GroupWander : MonoBehaviour
{
    public float wanderSpeed = 2f;
    public float damping = 0.1f; // Damping factor
    public GameObject wanderPoint; // Array of waypoints in the pathfinding area

    
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    public GameObject squadPos;
    public float centerDistance;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, squadPos.transform.position) > centerDistance)
        {
            CalculatePathToGroup();
        }

        
    }

    void FixedUpdate()
    {
        // Check if the path calculation is done
        if (path == null)
            return;

        if (currentWaypoint < path.vectorPath.Count)
        {
            // Move towards the current waypoint
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)rb.position).normalized;
            rb.velocity = direction * wanderSpeed;

            // Check if the player is close enough to the current waypoint to move to the next one
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < 0.1f)
            {
                currentWaypoint++;

                if (currentWaypoint == path.vectorPath.Count)
                {
                    rb.velocity = Vector2.zero;
                    path = null;
                }
            }
        }
    }

    void CalculatePathToGroup()
    {
        seeker.StartPath(rb.position, squadPos.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        // A* path calculation is complete, update the path
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

