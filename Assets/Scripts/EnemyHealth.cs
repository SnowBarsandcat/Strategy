using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public UnitAnimator unitAnimator;
    public int Health;
    public GameObject HealthBar;
    private HealthBar _healthBar;
    private int _maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = Health;
        _healthBar = HealthBar.GetComponent<HealthBar>();
    }

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        if (Health <= 0)
        {
            if (unitAnimator)
            {
                unitAnimator.Deat();
                
            }
            else
            {
                Destroy(gameObject);
            }
            

        }
    }
}
