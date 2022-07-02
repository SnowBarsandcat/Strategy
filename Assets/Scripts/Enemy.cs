using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    idle,
    walktobuilding,
    walktounit,
    attack
}
public class Enemy : MonoBehaviour
{
   
    public EnemyState curentEnemyState;

    public NavMeshAgent NavMeshAgent;
    public Building TargetBuilding;
    public Unit TargetUnit;
    public float DistanceToFollow = 7f;
    public float DistanceAttack = 1f;

    public float AttackPeriod = 1f;
    public int Damage = 1;
    private float _timer;

    public UnitAnimator unitAnimator;


    void Start()
    {
        SetState(EnemyState.walktobuilding);
        
       // GameObject healtBar = Instantiate(HealthBar);
        
       // _healthBar.Setup(transform);
    }
   
  

    void Update()
    {
        if (curentEnemyState == EnemyState.idle)
        {
            FindClosestBuilding();
            if (TargetBuilding)
            {
                SetState(EnemyState.walktobuilding);
            }
            FindClosestUnit();
        }
        else if (curentEnemyState == EnemyState.walktobuilding)
        {
           
            FindClosestUnit();
            if(TargetBuilding == null)
            {
                SetState(EnemyState.idle);

            }
            float distance = Vector3.Distance(transform.position, TargetBuilding.transform.position);
            if(distance < DistanceAttack)
            {
                SetState(EnemyState.attack);
            }
        }
        else if (curentEnemyState == EnemyState.walktounit)
        {
            if (TargetUnit)
            {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance < DistanceAttack)
                {
                    SetState(EnemyState.attack);
                }
                if (distance > DistanceToFollow)
                {
                    SetState(EnemyState.walktobuilding);
                }
            }
            else
            {
                SetState(EnemyState.walktobuilding);
            }
       }
        else if (curentEnemyState == EnemyState.attack)
        {
            if (TargetUnit)
            {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceAttack)
                {
                    SetState(EnemyState.walktounit);
                }
                _timer += Time.deltaTime;

                if (_timer > AttackPeriod)
                {
                    _timer = 0;
                    unitAnimator.Attack();
                    TargetUnit.TakeDamage(Damage);
                   
                }
            }
            else if (TargetBuilding)
            {
                FindClosestUnit();
                _timer += Time.deltaTime;
                if (_timer > AttackPeriod)
                {
                    _timer = 0;
                    unitAnimator.Attack();
                    TargetBuilding.TakeDamage(Damage);
                }
            }
            else
            {
                SetState(EnemyState.walktobuilding);
            }
        }
    }
    void SetState(EnemyState enemyState)
    {
        curentEnemyState = enemyState;
        if (curentEnemyState == EnemyState.idle)
        {
            unitAnimator.Idel();
        }
        else if (curentEnemyState == EnemyState.walktobuilding)
        {
            FindClosestBuilding();
            if (TargetBuilding)
            {
                NavMeshAgent.SetDestination(TargetBuilding.transform.position);
                unitAnimator.Walk();
            }
            else
            {
                SetState(EnemyState.idle);
            }
          
        }
        else if (curentEnemyState == EnemyState.walktounit)
        {
            unitAnimator.Walk();
        }
        else if (curentEnemyState == EnemyState.attack)
        {
            _timer = 0;
            unitAnimator.Attack();
        }
    }
    public void FindClosestBuilding()
    {
        Building[] allBuilding = FindObjectsOfType<Building>();
        float miDistance = Mathf.Infinity;
        Building closestBulding = null;

        for (int i = 0; i < allBuilding.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allBuilding[i].transform.position);
            if (distance < miDistance && allBuilding[i].CurentBuildingState == BuildingState.Placed)
            {
                miDistance = distance;
                closestBulding = allBuilding[i];
            }
            
        }
        TargetBuilding = closestBulding;
        
    }

    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        float miDistance = Mathf.Infinity;
        Unit closestUnit = null;

        for (int i = 0; i < allUnits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if (distance < miDistance)
            {
                miDistance = distance;
                closestUnit = allUnits[i];
            }
        }
        if (miDistance < DistanceToFollow)
        {
            TargetUnit = closestUnit;
            SetState(EnemyState.walktounit);
        }
    }

#if UNITY_EDITOR    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceAttack);

        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
#endif
}
