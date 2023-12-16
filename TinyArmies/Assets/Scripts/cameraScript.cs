using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Vector3 clickedPosition;

    private Vector3 newPos;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(2))
        {
            newPos = (clickedPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition));

            transform.position += newPos;
        }
    }
}
