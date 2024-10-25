using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy _enemy;
    private float _attackRange = 2f; // ��������� �����

    public ChaseState(Enemy enemy)
    {
        _enemy = enemy;
        _attackRange = _enemy.EnemySettings.GetAttackRadius();
    }

    public void OnEnter()
    {
        _enemy.SetAnimation(EnemyAnimation.Run); // ������������� �������� �������������
        _enemy.ResumeMovement();
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // ���������� �������� ��� ������ �� ���������
    }

    public void Tick()
    {
        Vector3 playerPosition = _enemy.GetPlayerPosition();

        if (_enemy.IsPlayerVisible())
        {
            // ��������� � ������� ������
            _enemy.MoveTowards(playerPosition, _enemy.EnemySettings.GetRunSpeed());

            // ��������� ���������� �� ������ ��� �����
            if (Vector3.Distance(_enemy.transform.position, playerPosition) <= _attackRange)
            {
                _enemy.ChangeState(new AttackState(_enemy)); // ������� � ��������� �����
            }
        }
        else
        {
            // ���� ����� ������ �� �����, ������������ � ��������� �������������
            _enemy.ChangeState(new LookAroundState(_enemy, _enemy.GetPlayerLastKnownPosition()));
        }
    }
}
