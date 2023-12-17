using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float chaseSpeed = 5f;
    public Transform player;

    private GroupWander groupWander;

    void Start()
    {
        groupWander = GetComponent<GroupWander>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Switch to chasing mode using A* pathfinding
            groupWander.enabled = false;
            MoveToPlayer();
        }
        else
        {
            // Switch back to wandering mode
            groupWander.enabled = true;
        }
    }

    void MoveToPlayer()
    {
        Vector2 playerPosition = player.position;
        Vector2 currentPosition = transform.position;

        // Calculate direction towards the player
        Vector2 directionToPlayer = (playerPosition - currentPosition).normalized;

        // Move towards the player
        transform.Translate(directionToPlayer * chaseSpeed * Time.deltaTime);
    }

}
