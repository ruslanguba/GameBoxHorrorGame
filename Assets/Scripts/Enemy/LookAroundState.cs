using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundState : IEnemyState
{
    private Enemy _enemy;
    private Vector3 _target; // Цель поворота (игрок)
    private float _lookAroundDuration = 12f; // Общее время на осмотр
    private float _lookAroundTimer = 0f; // Таймер для отслеживания времени осмотра
    private bool _isLookingLeft = true; // Флаг для отслеживания направления вращения
    private float _turnDuration = 4f; // Время на поворот (в каждую сторону)
    private float _rotationAngle = 45f; // Угол поворота
    private float _currentTurnDuration = 0f; // Таймер для текущего поворота

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
        _lookAroundTimer = 0f; // Сбрасываем таймер осмотра
        _currentTurnDuration = 0f; // Сбрасываем таймер для текущего поворота
        _enemy.SetAnimation(EnemyAnimation.LookAround); // Устанавливаем анимацию осмотра
        if (_target != null)
        {
            Vector3 directionToTarget = _target - _enemy.transform.position;
            directionToTarget.y = 0; // Игнорируем высоту
            _enemy.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }

    public void OnExit()
    {
        _enemy.ResetAnimation(); // Сбрасываем анимацию при выходе из состояния
    }

    public void Tick()
    {
        _lookAroundTimer += Time.deltaTime; // Увеличиваем общий таймер осмотра

        // Проверяем условия для перехода в другие состояния
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
        // Если время осмотра еще не истекло
    }

    private void LookAround() 
    {
        // Если время осмотра еще не истекло
        if (_lookAroundTimer < _lookAroundDuration)
        {
            // Управляем вращением
            if (_isLookingLeft)
            {
                _currentTurnDuration += Time.deltaTime;
                _enemy.transform.Rotate(Vector3.up, _rotationAngle * Time.deltaTime / _turnDuration); // Вращение влево
                if (_currentTurnDuration >= _turnDuration) // Если прошло время поворота
                {
                    _isLookingLeft = false; // Меняем направление
                    _currentTurnDuration = 0f; // Сбрасываем таймер для следующего поворота
                }
            }
            else
            {
                _currentTurnDuration += Time.deltaTime;
                _enemy.transform.Rotate(Vector3.up, -_rotationAngle * Time.deltaTime / (_turnDuration / 2)); // Вращение вправо
                if (_currentTurnDuration >= _turnDuration) // Если прошло время поворота
                {
                    _isLookingLeft = true; // Возвращаемся к первому направлению
                    _currentTurnDuration = 0f; // Сбрасываем таймер для следующего поворота
                }
            }
        }
        else
        {
            // Переход в состояние патрулирования после завершения осмотра
            _enemy.ChangeState(new PatrolState(_enemy, _enemy.GetPatrolTargetPoint()));
            return;
        }
    }
}
