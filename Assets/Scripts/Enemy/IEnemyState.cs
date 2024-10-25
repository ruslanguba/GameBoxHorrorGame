using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void OnEnter();  // Что делать при входе в состояние
    void OnExit();   // Что делать при выходе из состояния
    void Tick();     // Логика выполнения в текущем кадре
}
