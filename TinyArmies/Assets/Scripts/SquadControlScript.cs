using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SquadControlScript : MonoBehaviour
{
    //5E5E5E
    [SerializeField] private Color normalColor;
    //00FDFF
    [SerializeField] private Color selectColor;

    public float moveSpeed = 5f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;

    public bool selected;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CalculatePathToMouse();
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

    void CalculatePathToMouse()
    {
        if(ArmyControllerScript.Instance.selected == gameObject)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            seeker.StartPath(rb.position, mousePos, OnPathComplete);
        }
        
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

    void OnMouseDown()
    {
        ArmyControllerScript.Instance.Select(gameObject);
        gameObject.GetComponent<SpriteRenderer>().color = selectColor;

        Debug.Log("selected");
    }

    public void Deselect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = normalColor;
    }


}
