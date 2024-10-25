using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthSystem))]
public class CharacterHealth : MonoBehaviour
{
    private HealthSystem _healthSystem => GetComponent<HealthSystem>();
    public Action<float> OnHealthChange;

    public void TakeDamage(float damage)
    {
        _healthSystem.TakeDamage(damage);
        OnHealthChange?.Invoke(_healthSystem.GetCurrentHealth());
    }

    public void Heal(float amount)
    {
        _healthSystem.Heal(amount);
        OnHealthChange?.Invoke(_healthSystem.GetCurrentHealth());
    }
}
