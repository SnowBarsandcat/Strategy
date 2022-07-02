using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    private GameObject _minimapCamera;
    private float _minimapSize;
    private Vector3 _tempV3;
    void Start()
    {
        _minimapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
        

    }


    private void Update()
    {
        _minimapSize = _minimapCamera.GetComponent<Camera>().orthographicSize;
        _tempV3 = transform.parent.transform.position;
        _tempV3.y = transform.position.y;
        transform.position = _tempV3;
    }
    void LateUpdate()
    {
        float minX = _minimapCamera.transform.transform.position.x - _minimapSize;
        float maxX = _minimapCamera.transform.transform.position.x + _minimapSize;
        float minZ = _minimapCamera.transform.transform.position.z - _minimapSize;
        float maxZ = _minimapCamera.transform.transform.position.z + _minimapSize;

        transform.position = new Vector3
            (Mathf.Clamp(transform.position.x, minX, maxX),transform.position.y, 
            Mathf.Clamp(transform.position.z, minZ, maxZ));
    }
}
