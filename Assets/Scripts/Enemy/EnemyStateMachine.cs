using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState _currentState;

    private void Start()
    {

    }

    public void SetState(IEnemyState newState)
    {
        _currentState?.OnExit(); // ��������� ������� ���������
        _currentState = newState;
        _currentState.OnEnter(); // ������ � ����� ���������
    }

    private void Update()
    {
        _currentState?.Tick(); // ��������� �������� �������� ��������� ������ ����
    }
}
