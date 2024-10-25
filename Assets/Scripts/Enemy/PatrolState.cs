using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _targetPoint;

    public PatrolState(Enemy enemy, Vector3 targetPoint)
    {
        _enemy = enemy;
        _targetPoint = targetPoint;
    }

    public void OnEnter()
    {
        _enemy.SetAnimation(EnemyAnimation.Walk); // ”станавливаем анимацию движени€
        _enemy.ResumeMovement();
        _enemy.MoveTowards(_targetPoint, _enemy.EnemySettings.GetWalkSpeed());
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // —брасываем анимацию при выходе из состо€ни€
    }

    public void Tick()
    {
        // ѕровер€ем услови€ дл€ перехода в другие состо€ни€
        if (_enemy.IsPlayerVisible())
        {
            _enemy.ChangeState(new ChaseState(_enemy));
            return;
        }
        else if (_enemy.IsPlayerHeard())
        {
            Vector3 lastKnownPosition = _enemy.GetPlayerLastKnownPosition();
            _enemy.ChangeState(new InvestigateState(_enemy, lastKnownPosition, _enemy.InvestigationRadius));
            return;
        }

        // ƒвигаемс€ к текущей точке патрулировани€
        if (Vector3.Distance(_enemy.transform.position, _targetPoint) < 0.1f)
        {
            _enemy.ChangeState(new IdleState(_enemy)); // ѕереход в состо€ние поко€
        }
    }
}
