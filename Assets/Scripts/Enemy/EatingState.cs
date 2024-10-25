using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingState : IEnemyState
{
    private Enemy _enemy;

    public EatingState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.StopMovement(); // Останавливаем движение врага
        _enemy.SetAnimation(EnemyAnimation.Eat); // Устанавливаем анимацию покоя
        Debug.Log("Eating State");
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (_enemy.IsPlayerHeard())
        {
            Debug.Log("Enemey hear player");
            Vector3 lastKnownPosition = _enemy.GetPlayerLastKnownPosition();
            _enemy.ChangeState(new GetUpState(_enemy, lastKnownPosition));
            return;
        }
    }
}
