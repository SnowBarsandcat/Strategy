using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MM : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum MinimapState
    {
        Hover,
        UnHover,
        Other


    }
    public MinimapState CurentMiniMapState = MinimapState.Other;
    private Camera _mainCamera;
    private Camera _MiniMapCamera;
    public Camera RayCastCamera;
    private Vector3 _startPossition;
    private Vector3 _cameraStartPosition;
    private Plane _plane;
    private bool isMoving = false;
   // private GameObject _ground;
    public Canvas CanvasFE;
    private cb _cb;
    public GameObject FrameImageCanvas;
    public int WidthWorld = 100;
    public int LenghtWorld  = 100;
    void Start()
    {
        _mainCamera = Camera.main;
        _MiniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        //_ground = GameObject.FindGameObjectWithTag("Ground");
        _plane = new Plane(Vector3.up, Vector3.zero);
        _cb = _mainCamera.GetComponentInChildren<cb>();
    }


    void Update()
    {
        
        if (CurentMiniMapState == MinimapState.Hover)
        {
            MovementMM();
            ScalingMiniMap();
            ClickMinimap();
        }

    }
    void MovementMM()
    {
        
            Ray ray = RayCastCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            _plane.Raycast(ray, out distance);
            Vector3 point = ray.GetPoint(distance);

            if (Input.GetMouseButtonDown(2))
            {
                _startPossition = point;
                _cameraStartPosition = _MiniMapCamera.transform.position;
            isMoving = true;
            }
            if (Input.GetMouseButton(2) && isMoving)
            {
            
                Vector3 offset = point - _startPossition;
                _MiniMapCamera.transform.position = _cameraStartPosition - offset;
          
            }
        if (Input.GetMouseButtonUp(2))
        {
            isMoving = false;

        }
        float groundSizeX = WidthWorld;
        float limitPositionX = groundSizeX / 2 - _MiniMapCamera.orthographicSize;
        float groundSizeZ = LenghtWorld;
        float limitPositionZ= groundSizeZ / 2 - _MiniMapCamera.orthographicSize;

        _MiniMapCamera.transform.position = new Vector3
            (Mathf.Clamp(_MiniMapCamera.transform.position.x, 
            -limitPositionX, limitPositionX), _MiniMapCamera.transform.position.y,
            Mathf.Clamp
            (_MiniMapCamera.transform.position
            .z, -limitPositionZ, limitPositionZ));
      


      


    }
    void ScalingMiniMap()
    {
        float mingroungSize = Mathf.Min(WidthWorld , LenghtWorld);
        _MiniMapCamera.orthographicSize -= Input.mouseScrollDelta.y;
        _MiniMapCamera.orthographicSize = Mathf.Clamp(_MiniMapCamera.orthographicSize, 10, mingroungSize / 2);
        RayCastCamera.orthographicSize = _MiniMapCamera.orthographicSize;


    }
    void ClickMinimap()
    {
        if (Input.GetMouseButton(0))
        {
            Rect miniMapRect = GetComponent<RectTransform>().rect;
            Vector3 mousePose = Input.mousePosition;
            mousePose.x -= transform.position.x;
            mousePose.y -= transform.position.y;
            Debug.Log(mousePose.x / miniMapRect.width * 2 * _MiniMapCamera.orthographicSize);
            _mainCamera.transform.position = new Vector3(
            mousePose.x / miniMapRect.width * 2 * _MiniMapCamera.orthographicSize + _MiniMapCamera.transform.position.x, _mainCamera.transform.position.y,
           mousePose.y / miniMapRect.height * 2 * _MiniMapCamera.orthographicSize - _cb.OffSetPOsitionZ + _MiniMapCamera.transform.position.z);
           // Debug.Log(transform.position);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mainCamera.GetComponent<CameraMove>().enabled = false;
        CurentMiniMapState = MinimapState.Hover;
        _mainCamera.GetComponent<CameraMove>().isMoving= false;
        CanvasFE.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mainCamera.GetComponent<CameraMove>().enabled = true;
        CurentMiniMapState = MinimapState.UnHover;
        isMoving = false;
        CanvasFE.gameObject.SetActive(true);
    }
}
