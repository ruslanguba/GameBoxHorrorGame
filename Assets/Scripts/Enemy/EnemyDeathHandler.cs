using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeathControll;

public class EnemyDeathHandler : DeathHandler
{
    [SerializeField] private Enemy _enemy;
    protected override void Start()
    {
        base.Start();
        _enemy = GetComponent<Enemy>();
    }

    protected override void HandleDeath()
    {
        _enemy.ChangeState(new DeathState(_enemy));
    }
}
