using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject SelectionInd;
    
    public virtual void Start()
    {
        UnSelection();
    }
    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f; 
    }
    public virtual void OnUnhover()
    {
        transform.localScale = Vector3.one;
    }
    public virtual void Selection()
    {
        SelectionInd.SetActive(true);
         
    }
    public virtual void UnSelection()
    {
        SelectionInd.SetActive(false);
       
    }
    public virtual void WhenClickOnGround(Vector3 point)
    {

    }

}
