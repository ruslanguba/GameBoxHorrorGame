using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DeathControll
{
    public class DeathHandler : MonoBehaviour
    {
        //[SerializeField] private AudioSource _deathSound; // Звук смерти
        [SerializeField] private HealthSystem _healthSystem;

        virtual protected void Start()
        {
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.OnDeath.AddListener(HandleDeath);
        }

        virtual protected void HandleDeath()
        {
        }
    }
}
