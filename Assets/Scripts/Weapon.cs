using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage = 25f;
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected bool _isLocked = true;
    public abstract void Attack();

    public WeaponType GetWeaponType() {  return weaponType; }
    public bool IsWeaponLocked()
    {
        return _isLocked;
    }
    public void UnlockWeapon()
    {
        _isLocked = false;
    }
}
