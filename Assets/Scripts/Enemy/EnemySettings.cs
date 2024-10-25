using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 1.5f;
    [SerializeField] private float _runSpeed = 2.5f;
    public float GetWalkSpeed() 
    {
        return _walkSpeed;
    }
    public float GetRunSpeed()
    {
        return _runSpeed;
    }

    [Header("Timings")]
    [SerializeField] private float _lookArroundTime = 6;
    [SerializeField] private float _idleTime = 1;

    public float GetIdleArroundTime()
    {
        return _lookArroundTime;
    }
    public float IdleTime()
    {
        return _idleTime;
    }

    [Header("Damage")]
    [SerializeField] private float _damage = 15;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _attackSpeed = 1;
    public float GetAttackSpeed() 
    {
        return _attackSpeed; 
    }
    public float GetDamage() 
    {
        return _damage; 
    }
    public float GetAttackRadius() 
    { 
        return _attackRadius; 
    }
}
