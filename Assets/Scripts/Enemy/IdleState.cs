using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : IEnemyState
{
    private Enemy _enemy;
    private float _idleDuration = 2; // Время, проведенное в состоянии покоя перед осмотром
    private float _idleTimer = 0; // Таймер для отслеживания времени в состоянии покоя

    public IdleState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _idleTimer = 0f; // Сбрасываем таймер покоя
        _enemy.StopMovement(); // Останавливаем движение врага
        _enemy.SetAnimation(EnemyAnimation.Idle); // Устанавливаем анимацию покоя
        Debug.Log("Entering Idle State");
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // Сбрасываем анимацию при выходе из состояния
    }

    public void Tick()
    {
        // Проверяем условия перехода в другие состояния
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

        // Увеличиваем таймер для состояния покоя
        _idleTimer += Time.deltaTime;

        // Если таймер покоя превышает заданное время, переходим в состояние осмотра
        if (_idleTimer >= _idleDuration)
        {
            _enemy.ChangeState(new LookAroundState(_enemy)); // Переход в состояние осмотра
        }
    }
}

