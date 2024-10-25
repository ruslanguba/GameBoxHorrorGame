using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private List<GameObject> _weaponInHandList;
    [SerializeField] private CharacterAttack _characterAttack;
    private Weapon _currentWeapon;

    private void Awake()
    {
        _characterAttack = GetComponent<CharacterAttack>();
        _currentWeapon = _weaponList[0];
    }
    private void Start()
    {
        Inventory.Instance.OnNewWeaponFound.AddListener(UnlockWeapon);
    }

    public void UnlockWeapon(WeaponType weaponItem) {
        foreach (Weapon weapon in _weaponList)
        {
            if (weapon.GetWeaponType() == weaponItem)
            {
                weapon.UnlockWeapon();
            }
        }
        if (_characterAttack.GetCurrentWeapon() == null)
        {
            ChangeWeapon(((int)weaponItem));
        }
    }

    public void SetCharacterWeapon()
    {
        _characterAttack.SetCurrentWeapon(_currentWeapon);
    }

    private void SetActiveCharacterWeapon(int index)
    {
        foreach (GameObject weapon in _weaponInHandList)
        {
            weapon.SetActive(false);
        }
        _weaponInHandList[index - 1].SetActive(true);
    }

    public void ChangeWeapon(int index)
    {
        if (index > _weaponList.Count || _weaponList[index - 1].IsWeaponLocked())
        {
            return;
        }
        else
        {
            _currentWeapon = _weaponList[index - 1];
            SetCharacterWeapon();
            SetActiveCharacterWeapon(index);
        }
    }
}
