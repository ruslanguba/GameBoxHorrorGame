using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _target; // ���� �������� (�����)
    private float _lookAroundDuration = 12f; // ����� ����� �� ������
    private float _lookAroundTimer = 0f; // ������ ��� ������������ ������� �������
    private bool _isLookingLeft = true; // ���� ��� ������������ ����������� ��������
    private float _turnDuration = 4f; // ����� �� ������� (� ������ �������)
    private float _rotationAngle = 45f; // ���� ��������
    private float _currentTurnDuration = 0f; // ������ ��� �������� ��������

    public LookAroundState(Enemy enemy)
    {
        _enemy = enemy;
    }
    public LookAroundState(Enemy enemy, Vector3 target)
    {
        _enemy = enemy;
        _target = target;
    }
    public void OnEnter()
    {
        _lookAroundTimer = 0f; // ���������� ������ �������
        _currentTurnDuration = 0f; // ���������� ������ ��� �������� ��������
        _enemy.SetAnimation(EnemyAnimation.LookAround); // ������������� �������� �������
        if (_target != null)
        {
            Vector3 directionToTarget = _target - _enemy.transform.position;
            directionToTarget.y = 0; // ���������� ������
            _enemy.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // ���������� �������� ��� ������ �� ���������
    }

    public void Tick()
    {
        _lookAroundTimer += Time.deltaTime; // ����������� ����� ������ �������

        // ��������� ������� ��� �������� � ������ ���������
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
        LookAround();
        // ���� ����� ������� ��� �� �������
    }

    private void LookAround() 
    {
        // ���� ����� ������� ��� �� �������
        if (_lookAroundTimer < _lookAroundDuration)
        {
            // ��������� ���������
            if (_isLookingLeft)
            {
                _currentTurnDuration += Time.deltaTime;
                _enemy.transform.Rotate(Vector3.up, _rotationAngle * Time.deltaTime / _turnDuration); // �������� �����
                if (_currentTurnDuration >= _turnDuration) // ���� ������ ����� ��������
                {
                    _isLookingLeft = false; // ������ �����������
                    _currentTurnDuration = 0f; // ���������� ������ ��� ���������� ��������
                }
            }
            else
            {
                _currentTurnDuration += Time.deltaTime;
                _enemy.transform.Rotate(Vector3.up, -_rotationAngle * Time.deltaTime / (_turnDuration / 2)); // �������� ������
                if (_currentTurnDuration >= _turnDuration) // ���� ������ ����� ��������
                {
                    _isLookingLeft = true; // ������������ � ������� �����������
                    _currentTurnDuration = 0f; // ���������� ������ ��� ���������� ��������
                }
            }
        }
        else
        {
            // ������� � ��������� �������������� ����� ���������� �������
            _enemy.ChangeState(new PatrolState(_enemy, _enemy.GetPatrolTargetPoint()));
            return;
        }
    }
}
