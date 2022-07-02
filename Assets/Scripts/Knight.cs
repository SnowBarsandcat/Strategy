using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public enum UnitState
{
    idle,
    WalkToPoint,
    WalkToEnemy,
    attack
}
public class Knight : Unit
{
  
    public UnitState curentUnitState;

    
    
    //public Building TargetBuilding;
    public Vector3 TargetPoint;
    public EnemyHealth TargetEnemy;
    public float DistanceToFollow = 7f;
    public float DistanceAttack = 1f;

    public float AttackPeriod = 1f;
    public int Damage = 1;
    private float _timer;

   
    public override void Start()
    {
        base.Start();
        SetState(UnitState.idle
            );
    }

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        SetState(UnitState.WalkToPoint
           );
    }
    void Update()
    {
        if (curentUnitState == UnitState.idle)
        {
            FindClosestEnemy();
        }
        else if (curentUnitState == UnitState.WalkToPoint)
        {
          
            FindClosestEnemy();
            if (IsTargetPointReached())
            {
                SetState(UnitState.idle);
            }

        }
        else if (curentUnitState == UnitState.WalkToEnemy)
        {
            if (TargetEnemy)
            {
                NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance < DistanceAttack)
                {
                    SetState(UnitState.attack);
                }
                if (distance > DistanceToFollow)
                {
                    SetState(UnitState.WalkToPoint);
                }
            }
            else
            {
                SetState(UnitState.WalkToPoint);
            }
        }
        else if (curentUnitState == UnitState.attack)
        {
            if (TargetEnemy)
            {
                ResetTargetPoint();
                NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > DistanceAttack)
                {
                    SetState(UnitState.WalkToEnemy);
                }
                _timer += Time.deltaTime;

                if (_timer > AttackPeriod)
                {
                    unitAnimator.Attack();
                    _timer = 0;
                    TargetEnemy.TakeDamage(Damage);
                }
            }
            else
            {
                SetState(UnitState.WalkToPoint);
            }
        }
    }
    void SetState(UnitState UnitState)
    {
        curentUnitState = UnitState;
        if (curentUnitState == UnitState.idle)
        {
            unitAnimator.Idel();
        }
        else if (curentUnitState == UnitState.WalkToPoint)
        {
            unitAnimator.Walk();
        }
        else if (curentUnitState == UnitState.WalkToEnemy)
        {
            unitAnimator.Walk();
            ResetTargetPoint();
        }
        else if (curentUnitState == UnitState.attack)
        {
            unitAnimator.Attack();
            _timer = 0;
            ResetTargetPoint();
        }
    }
   
    public void FindClosestEnemy()
    {
       EnemyHealth[] allEnemys = FindObjectsOfType<EnemyHealth>();
       float miDistance = Mathf.Infinity;
       EnemyHealth   closestEnemy = null;

        for (int i = 0; i < allEnemys.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allEnemys[i].transform.position);
            if (distance < miDistance)
            {
                miDistance = distance;
                closestEnemy = allEnemys[i];
            }
        }
        if (miDistance < DistanceToFollow)
        {
            TargetEnemy = closestEnemy;
            SetState(UnitState.WalkToEnemy);
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
 

