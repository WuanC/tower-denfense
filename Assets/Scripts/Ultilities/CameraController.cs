using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 difference;
    bool isDragging;
    [SerializeField] float speed = 0.1f;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            isDragging = true;
            return;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            difference = Vector3.zero;
            isDragging = false;
            return;
        }
        if(isDragging) {

            Vector3 tmp1 = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
            Vector3 tmp = tmp1 - difference;
            Camera.main.transform.position -= tmp * speed;
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
        }
    }

}
