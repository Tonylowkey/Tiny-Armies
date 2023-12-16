using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    public bool In;
    public float speed;

    public Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            In = true;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (In == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, mousePos) < 0.1f)
            {
                In = false;
            }
        }
    }
}
