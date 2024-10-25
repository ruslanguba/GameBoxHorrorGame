using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy _enemy;
    private float _attackRange = 2f; // ƒальность атаки

    public ChaseState(Enemy enemy)
    {
        _enemy = enemy;
        _attackRange = _enemy.EnemySettings.GetAttackRadius();
    }

    public void OnEnter()
    {
        _enemy.SetAnimation(EnemyAnimation.Run); // ”станавливаем анимацию преследовани€
        _enemy.ResumeMovement();
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // —брасываем анимацию при выходе из состо€ни€
    }

    public void Tick()
    {
        Vector3 playerPosition = _enemy.GetPlayerPosition();

        if (_enemy.IsPlayerVisible())
        {
            // ƒвигаемс€ к позиции игрока
            _enemy.MoveTowards(playerPosition, _enemy.EnemySettings.GetRunSpeed());

            // ѕровер€ем рассто€ние до игрока дл€ атаки
            if (Vector3.Distance(_enemy.transform.position, playerPosition) <= _attackRange)
            {
                _enemy.ChangeState(new AttackState(_enemy)); // ѕереход в состо€ние атаки
            }
        }
        else
        {
            // ≈сли игрок больше не виден, возвращаемс€ в состо€ние расследовани€
            _enemy.ChangeState(new LookAroundState(_enemy, _enemy.GetPlayerLastKnownPosition()));
        }
    }
}
