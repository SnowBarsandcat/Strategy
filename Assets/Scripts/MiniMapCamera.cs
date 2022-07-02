using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
   
    void Start()
    {
        Camera targetCamera = GetComponent<Camera>();
        targetCamera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }

  
}
