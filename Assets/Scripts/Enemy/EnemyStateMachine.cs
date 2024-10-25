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
        _currentState?.OnExit(); // Завершаем текущее состояние
        _currentState = newState;
        _currentState.OnEnter(); // Входим в новое состояние
    }

    private void Update()
    {
        _currentState?.Tick(); // Выполняем действия текущего состояния каждый кадр
    }
}
