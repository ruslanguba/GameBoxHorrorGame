
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    [SerializeField] private WeaponType _weaponType;  
    protected override void Collect()
    {
        Inventory.Instance.AddWeapon(_weaponType);
    }

    public WeaponType GetWeaponType
    {
        get { return _weaponType; }
    }
}
