using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item
{
    [SerializeField] private float _amount = 50;

    public float GetAmount()
    {
        return _amount;
    }
}
