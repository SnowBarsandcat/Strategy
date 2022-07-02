using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera RayCastCamera;
    private Vector3 _startPossition;
    private Vector3 _cameraStartPosition;
    private Plane _plane;
    public bool isMoving = false;
    private void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
    void Update()
    {
       Ray ray = RayCastCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);

        if (Input.GetMouseButtonDown(2))
        {
            _startPossition = point;
            _cameraStartPosition = transform.position;
            isMoving = true;
        }
        if (Input.GetMouseButton(2) && isMoving)
        {
            Vector3 offset = point - _startPossition;
            transform.position = _cameraStartPosition - offset;
            
        }
        if (Input.GetMouseButtonUp(2))
        {
            isMoving = false;
        }
        
        Scale(transform);
        Scale(RayCastCamera.transform);
     

           

        void Scale(Transform transform)
        {
            transform.position -= new Vector3(0f, Input.mouseScrollDelta.y, 0);
            transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, -41, 41),
        Mathf.Clamp(transform.position.y, 8, 14),
        Mathf.Clamp(transform.position.z, -51, 35));
        }

    }
}
