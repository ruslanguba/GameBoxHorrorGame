using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : IEnemyState
{
    private Enemy _enemy;
    private float _idleDuration = 2; // �����, ����������� � ��������� ����� ����� ��������
    private float _idleTimer = 0; // ������ ��� ������������ ������� � ��������� �����

    public IdleState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _idleTimer = 0f; // ���������� ������ �����
        _enemy.StopMovement(); // ������������� �������� �����
        _enemy.SetAnimation(EnemyAnimation.Idle); // ������������� �������� �����
        Debug.Log("Entering Idle State");
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // ���������� �������� ��� ������ �� ���������
    }

    public void Tick()
    {
        // ��������� ������� �������� � ������ ���������
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

        // ����������� ������ ��� ��������� �����
        _idleTimer += Time.deltaTime;

        // ���� ������ ����� ��������� �������� �����, ��������� � ��������� �������
        if (_idleTimer >= _idleDuration)
        {
            _enemy.ChangeState(new LookAroundState(_enemy)); // ������� � ��������� �������
        }
    }
}

