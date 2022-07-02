using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BuildingState
{
    Placed,
    Purched
}
public class Building : SelectObject
{
    public int Price;
    public int Xsize = 3;
    public int Zsize = 3;
    public int Health;
    public GameObject HealthBar;
    private HealthBar _healthBar;
    private int _maxHealth;

    public GameObject MenuObject;

    private Color _startColor;
    public Renderer Renderer;

    public BuildingState CurentBuildingState = BuildingState.Purched;

    private NavMeshObstacle _navMeshObstacle;

    public void Awake()
    {
        _startColor = Renderer.material.color;
    }
    public override void Start()
    {
        base.Start();
        UnSelection();
        _navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        _navMeshObstacle.enabled = false;

         _maxHealth = Health;
        _healthBar = HealthBar.GetComponent<HealthBar>();
    }

    public void DisplayUP()
    {
        // Renderer.material.color = Color.red;
        Renderer.material.color = new Color(_startColor.r,0,0,0.3f);
        }
    public void DisplayAP()
    {
        //Renderer.material.color = _startColor;
        Renderer.material.color = new Color(_startColor.r, _startColor.g, _startColor.b, 0.3f);
    
}
    private void OnDrawGizmos()
    {
        float cellSize = FindObjectOfType<BuildingPlaser>().CellSize;
        for (int X = 0; X < Xsize; X++)
        {
            for (int Z = 0; Z <Zsize; Z++)
            {
                Gizmos.DrawWireCube
                    (transform.position + new Vector3(X,0,Z) 
                    * cellSize ,new Vector3(1, 0, 1)
                    * cellSize);
            }
        }
        
    
    }
    public virtual void Builded()
    {
        FindObjectOfType<Resources>().Money -= Price;
        _navMeshObstacle.enabled = true;
        Renderer.material.color = _startColor;
    }
        
    public override void Selection()
    {
        base.Selection();
        MenuObject.SetActive(true);
    }
    public override void UnSelection()
    {
        base.UnSelection();
        MenuObject.SetActive(false);
    }
    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
