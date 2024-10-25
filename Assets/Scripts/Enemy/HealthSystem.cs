using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
        OnHit?.Invoke();
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}
