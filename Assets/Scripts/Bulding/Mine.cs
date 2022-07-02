using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    private Resources resourses;
    public int MoneyCount = 1;
    public float PeriodOfMine = 1;

    public override void Start()
    {
        base.Start();
        resourses = FindObjectOfType<Resources>();
        
    }
    public override void Builded()
    {
        base.Builded();
        InvokeRepeating(nameof(AddMoney), PeriodOfMine, PeriodOfMine);
    }
    
    private void AddMoney()
    {
        resourses.Money += MoneyCount;
    }
}
