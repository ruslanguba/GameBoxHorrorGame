using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : IEnemyState
{
    private Enemy _enemy;

    public DeathState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.StopMovement();
        _enemy.SetAnimation(EnemyAnimation.Die);
        _enemy.GetComponent<Collider>().enabled = false;
        Object.Destroy(_enemy.gameObject, 5f);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
