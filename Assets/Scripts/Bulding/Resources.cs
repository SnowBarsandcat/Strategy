using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    private int _money;
    public int StartMoneyBonus;
    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;
            MoneyText.text = _money.ToString();
        }

    }
        

    public Text MoneyText;

    private void Start()
    {
        _money += StartMoneyBonus;
        MoneyText.text = _money.ToString();

    }
}
