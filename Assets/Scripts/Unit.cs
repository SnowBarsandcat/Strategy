using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectObject
{
    public int Health;
    public NavMeshAgent NavMeshAgent;
    public int Price;
    public GameObject HealthBar;
    private HealthBar _healthBar;
    private int _maxHealth;
    public GameObject NavigationIndicator;
    public UnitAnimator unitAnimator;

    public override void Start()
    {
        base.Start();
        _maxHealth = Health;
     //  GameObject healtBar = Instantiate(HealthBarPrefab);
        _healthBar = HealthBar.GetComponent<HealthBar>();
        //_healthBar.Setup(transform);
        NavigationIndicator.SetActive(false);
        NavigationIndicator.transform.parent = null;

    }
    public void ResetTargetPoint()
    {
        NavigationIndicator.SetActive(false);
    }
    private void LateUpdate()
    {
        if (IsTargetPointReached())
        {
            ResetTargetPoint();
        }
    }

    public bool IsTargetPointReached()
    {
        if (NavMeshAgent.pathPending || NavMeshAgent.remainingDistance > NavMeshAgent.stoppingDistance)
        {
            return false;
        }

        return !NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f;
    }
    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        NavMeshAgent.SetDestination(point);
        NavigationIndicator.SetActive(true);
        NavigationIndicator.transform.position = new Vector3(point.x, NavigationIndicator.transform.position.y, point.z);
    }
    private void OnDestroy()
    {
        Management managment = FindObjectOfType<Management>();
        if (managment)
        {
            managment.Unselect(this);
        }
        if (NavigationIndicator)
        {
            Destroy(NavigationIndicator);
        }

    }
    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        _healthBar.SetHealth(Health,_maxHealth);
        if (Health <= 0)
        {
            unitAnimator.Deat();
        }
       
    }


}
