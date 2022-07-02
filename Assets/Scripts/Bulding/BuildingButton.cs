using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlaser BuildingPlaser;
    public GameObject BuildingPrefab;
    public Text PriceText;

    private void Start()
    {
        PriceText.text = BuildingPrefab.GetComponent<Building>().Price.ToString();
    }
    public void Trybuy()
    {
        int price = BuildingPrefab.GetComponent<Building>().Price;
        if ( FindObjectOfType<Resources>().Money >= price)
        {
            BuildingPlaser.CreateBulding(BuildingPrefab);
        }
        else
        {
            Debug.Log("not enogh money");
        }
    }
}
