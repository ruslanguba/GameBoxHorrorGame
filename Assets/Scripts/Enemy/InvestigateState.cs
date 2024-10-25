using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigateState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _lastKnownPosition; // Последняя известная позиция игрока
    private float _investigationRadius; // Радиус расследования
    private float _investigationTime; // Время, в течение которого враг будет расследовать
    private float _startTime; // Время начала расследования

    public InvestigateState(Enemy enemy, Vector3 lastKnownPosition, float investigationRadius)
    {
        _enemy = enemy;
        _lastKnownPosition = lastKnownPosition;
        _investigationRadius = investigationRadius;
        _investigationTime = 5f; // Время расследования
    }

    public void OnEnter()
    {
        _enemy.ResumeMovement();
        _enemy.MoveTowards(_lastKnownPosition, _enemy.Speed);
        _enemy.SetAnimation(EnemyAnimation.Walk);
        _startTime = Time.time; // Запоминаем время начала расследования
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // Сбрасываем анимацию при выходе из состояния
    }

    public void Tick()
    {
        if (Time.time >= _startTime + _investigationTime)
        {
            _enemy.ChangeState(new LookAroundState(_enemy)); // Возвращаемся к патрулированию
            return;
        }
        if (Time.time >= _startTime + _investigationTime)
        {
            _enemy.ChangeState(new PatrolState(_enemy, _enemy.GetPatrolTargetPoint())); // Возвращаемся к патрулированию
            return;
        }
        if (_enemy.IsPlayerVisible())
        {
            _enemy.ChangeState(new ChaseState(_enemy));
            return;
        }
        if (Vector3.Distance(_enemy.GetPlayerPosition(), _lastKnownPosition) <= _investigationRadius)
        {
            _enemy.ChangeState(new ChaseState(_enemy)); // Игрок найден, переходим в состояние преследования
            return;
        }
        _enemy.MoveTowards(_lastKnownPosition, _enemy.Speed);
    }
}
