using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGroup : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CalculateNewPath();
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
            rb.velocity = direction * moveSpeed;

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

    void CalculateNewPath()
    {
        Vector3 newPos = transform.position;

        newPos.x = Random.Range(0, GridManager.Instance.width);
        newPos.y = Random.Range(0, GridManager.Instance.height);

        seeker.StartPath(rb.position, newPos, OnPathComplete);
        
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
