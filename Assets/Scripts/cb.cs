using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cb : MonoBehaviour
{
    [HideInInspector]
    public float OffSetPOsitionZ;
    private Camera _miniMapCamera;
    // Start is called before the first frame update
    void Start()
    {
        Camera main = Camera.main;
        OffSetPOsitionZ = transform.position.z - main.transform.position.z;
        _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(
            _miniMapCamera.orthographicSize / 15,
            _miniMapCamera.orthographicSize / 15,
            _miniMapCamera.orthographicSize / 15);
    }

}
