using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _lastKnownPosition;
    private float _rotationSpeed = 30;
    private float _startTime;
    private float _animationEndTime  = 4;
    private bool _animationEnded;

    public GetUpState(Enemy enemy, Vector3 playerPosition)
    {
        _enemy = enemy;
        _lastKnownPosition = playerPosition;
        _animationEnded = false;
    }

    public void OnEnter()
    {
        _enemy.SetAnimation(EnemyAnimation.GetUp);
        _startTime = Time.time;
    }

    public void OnExit()
    {
        _enemy.ResumeMovement();
    }

    public void Tick()
    {
        if (!_animationEnded)
        {
            // Проверка, прошли ли 2 секунды с момента начала состояния
            if (Time.time >= _startTime + _animationEndTime)
            {
                _animationEnded = true;
            }
            return;
        }
        Vector3 direction = (_lastKnownPosition - _enemy.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _enemy.ChangeState(new LookAroundState(_enemy, _enemy.GetPlayerLastKnownPosition()));

    }
}
