using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    public void SetCurrentWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }
    public void Attack()
    {
        if(_currentWeapon != null) 
        {
            _currentWeapon.Attack();
        }
    }
}
