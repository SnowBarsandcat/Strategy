using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour
{

    public Camera Camera;
    public SelectObject Hoverd;
    public List<SelectObject> ListOfSelected = new List<SelectObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public bool isMoving = false;
    public MM _minimapCamera;
    public SelectionState CurrentSelectionState;
    private void Start()
    {
        
    }
    void Update()
    {

        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectebelColider>())
            {
                SelectObject hitSelected = hit.collider.GetComponent<SelectebelColider>().SelecetOb;
                if (Hoverd)
                {
                    if (Hoverd != hitSelected)
                    {
                        Hoverd.OnUnhover();
                        Hoverd = hitSelected;
                        Hoverd.OnHover();
                    }
                }
                else
                {
                    Hoverd = hitSelected;
                    Hoverd.OnHover();
                }
            }
            else
            {
                UnHoverCurrent();
            }

        }
        else
        {
            UnHoverCurrent();
        }
       
        if (Input.GetMouseButtonUp(1))
        {
            UnSelectAll();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Hoverd)
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    UnSelectAll();
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Slect(Hoverd);
            }
           
        }
        if(CurrentSelectionState == SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.tag == "Ground")
                {
                    int rowNumber = Mathf.CeilToInt(Mathf.Sqrt(ListOfSelected.Count));
                    for (int i = 0; i < ListOfSelected.Count; i++)
                    {
                        int row = i / rowNumber;
                        int column = i % rowNumber;
                        Vector3 point = hit.point + new Vector3(row, 0, column);
                        ListOfSelected[i].WhenClickOnGround(point);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0)&& _minimapCamera.CurentMiniMapState != MM.MinimapState.Hover)
        {
            _frameStart = Input.mousePosition;
            isMoving = true;
        }
        if (Input.GetMouseButton(0) && isMoving)
        {
          
                _frameEnd = Input.mousePosition;
                Vector2 min = Vector2.Min(_frameStart, _frameEnd);
                Vector2 max = Vector2.Max(_frameStart, _frameEnd);
                Vector2 size = max - min;
                if (size.magnitude > 10) 
            {
                CurrentSelectionState = SelectionState.Frame;
                FrameImage.enabled = true;
                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;

                UnSelectAll();
                Rect rect = new Rect(min, size);
                Unit[] allUnit = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnit.Length; i++)
                {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnit[i].transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Slect(allUnit[i]);
                    }
                }
                CurrentSelectionState = SelectionState.Other;

            }
          

        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            FrameImage.enabled = false;
            if(ListOfSelected.Count > 0)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentSelectionState = SelectionState.Other;
            }
        }
                
       




    }
    void Slect(SelectObject selectOb)
    {
        if (!ListOfSelected.Contains(selectOb))
        {


            ListOfSelected.Add(selectOb);
            selectOb.Selection();
        }
    }

    void UnHoverCurrent()
    {
        if (Hoverd)
        {
            Hoverd.OnUnhover();
            Hoverd = null;
        }
    }

    void UnSelectAll()
    {
        
            for (int i = 0; i < ListOfSelected.Count; i++)
            {
                ListOfSelected[i].UnSelection();
            }
            ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;

    }
    public void Unselect(SelectObject selectObject)
    {
        if (ListOfSelected.Contains(selectObject))
        {
            ListOfSelected.Remove(selectObject);
        }
    }
}



