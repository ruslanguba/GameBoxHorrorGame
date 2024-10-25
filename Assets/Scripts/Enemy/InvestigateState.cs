using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigateState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _lastKnownPosition; // ��������� ��������� ������� ������
    private float _investigationRadius; // ������ �������������
    private float _investigationTime; // �����, � ������� �������� ���� ����� ������������
    private float _startTime; // ����� ������ �������������

    public InvestigateState(Enemy enemy, Vector3 lastKnownPosition, float investigationRadius)
    {
        _enemy = enemy;
        _lastKnownPosition = lastKnownPosition;
        _investigationRadius = investigationRadius;
        _investigationTime = 5f; // ����� �������������
    }

    public void OnEnter()
    {
        _enemy.ResumeMovement();
        _enemy.MoveTowards(_lastKnownPosition, _enemy.Speed);
        _enemy.SetAnimation(EnemyAnimation.Walk);
        _startTime = Time.time; // ���������� ����� ������ �������������
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // ���������� �������� ��� ������ �� ���������
    }

    public void Tick()
    {
        if (Time.time >= _startTime + _investigationTime)
        {
            _enemy.ChangeState(new LookAroundState(_enemy)); // ������������ � ��������������
            return;
        }
        if (Time.time >= _startTime + _investigationTime)
        {
            _enemy.ChangeState(new PatrolState(_enemy, _enemy.GetPatrolTargetPoint())); // ������������ � ��������������
            return;
        }
        if (_enemy.IsPlayerVisible())
        {
            _enemy.ChangeState(new ChaseState(_enemy));
            return;
        }
        if (Vector3.Distance(_enemy.GetPlayerPosition(), _lastKnownPosition) <= _investigationRadius)
        {
            _enemy.ChangeState(new ChaseState(_enemy)); // ����� ������, ��������� � ��������� �������������
            return;
        }
        _enemy.MoveTowards(_lastKnownPosition, _enemy.Speed);
    }
}
