using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaser : MonoBehaviour
{
    public float CellSize = 1f;

    public Camera MainCamera;
    private Plane _plane;

    public Building CurrentBulding;

    public Dictionary<Vector2Int, Building> BuildingsDictionary
        = new Dictionary<Vector2Int, Building>();

    private void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
    void Update()
    {
        if (!CurrentBulding)
        {
            return;
            
           
        }
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentBulding != null)
        {

            Destroy(CurrentBulding.gameObject);
            CurrentBulding = null;
            return;
        }
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBulding.transform.position = new Vector3(x, 0, z) * CellSize;
        if (CheckAllow(x, z, CurrentBulding) )
         {
            CurrentBulding.DisplayAP();
            if (Input.GetMouseButtonDown(0))
            {
                InstaBulding(x, z, CurrentBulding);
                CurrentBulding = null;
            }
        }
        else
        {
            CurrentBulding.DisplayUP();
        }
        

    }
    public bool CheckAllow(int xPos, int zPos, Building building)
    {
        for (int x = 0; x < building.Xsize; x++)
        {
            for (int z = 0; z < building.Zsize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPos + x, zPos + z);
                if (BuildingsDictionary.ContainsKey(coordinate))
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void InstaBulding(int xPos , int zPos , Building building)
    {
        for (int x = 0; x <building.Xsize; x++)
        {
            for (int z = 0; z < building.Zsize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPos + x, zPos + z);
                BuildingsDictionary.Add(coordinate, building);
            }
        }
        building.CurentBuildingState = BuildingState.Placed;
        building.Builded();
       foreach(var item in BuildingsDictionary)
        {
            Debug.Log(item);
        }
    }
    public void CreateBulding (GameObject buildingPrefab)
    {
        GameObject newBuilding = Instantiate(buildingPrefab);
        CurrentBulding = newBuilding.GetComponent<Building>();
    }
}
