using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    private Animator _animator;
   
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

 
    public void Idel()
    {
        _animator.SetBool("Walk" , false);
    }
    public void Walk()
    {
        _animator.SetBool("Walk", true);
    }
    public void Attack()
    {
        _animator.SetTrigger("Attack");

    }
    public void Deat()
    {
        _animator.SetTrigger("Deat");

        Unit unit = GetComponentInParent<Unit>();
        Enemy enemy = GetComponentInParent<Enemy>();
        NavMeshAgent nma = GetComponentInParent<NavMeshAgent>();
        EnemyHealth enemyHealth = GetComponentInParent<EnemyHealth>();

        if (unit) Destroy(unit);
        if (enemy) Destroy(enemy);
        if (nma) Destroy(nma);
        if (enemyHealth) Destroy(enemyHealth);

    }
    public void DestroyEvent()
    {
        Destroy(transform.parent.gameObject);  
    }






}  

