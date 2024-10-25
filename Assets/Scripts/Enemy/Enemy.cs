using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IEnemyState _currentState;
    [SerializeField] private bool _isEating;
    private NavMeshAgent _navMeshAgent;
    private Transform _player; // Ссылка на игрока
    private float _hearingRange = 5f; // Дальность слуха

    private EnemyAnimatorController _enemyAnimatorController;
    private PatrolSystem _patrolSystem;
    private EnemyVision _enemyVision;
    private EnemyHearing _enemyHearing;

    public EnemySettings EnemySettings { get; private set; }
    public float Speed { get; private set; } = 1.2f; // Скорость движения врага
    public float StoppingDistance { get; private set; } = 0.5f; // Расстояние, на котором враг останавливается
    public float InvestigationRadius { get; private set; } = 5f; // Радиус расследования

    private Vector3 _lastKnownPlayerPosition; // Позиция последнего известного местоположения игрока

    private void Awake()
    {
        EnemySettings = GetComponent<EnemySettings>();
        _navMeshAgent = GetComponent<NavMeshAgent>(); // Получаем компонент NavMeshAgent
        _player = FindObjectOfType<Player>().transform; // Получаем ссылку на игрока
        _enemyVision = GetComponent<EnemyVision>();
        _enemyAnimatorController = GetComponent<EnemyAnimatorController>();
        _patrolSystem = GetComponent<PatrolSystem>();
        _enemyHearing = GetComponent<EnemyHearing>();
    }

    private void Start()
    {
        if (_isEating)
        {
            ChangeState(new EatingState(this));
        }
        else
        {
            ChangeState(new PatrolState(this, _patrolSystem.GetNextPatrolTarget())); // Начинаем с состояния патрулирования
        }
    }

    private void Update()
    {
        _currentState?.Tick(); // Обновляем текущее состояние
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState?.OnExit(); // Выход из текущего состояния
        _currentState = newState; // Устанавливаем новое состояние
        _currentState.OnEnter(); // Вход в новое состояниеS
    }

    public void SetAnimation(EnemyAnimation animation)
    {
        _enemyAnimatorController.SetAnimation(animation);
    }

    public void ResetAnimation()
    {
        _enemyAnimatorController.ResetAnimation();
    }
    public bool IsPlayerVisible()
    {
        if (_player == null)
        {
            return false;
        }
        return _enemyVision.CanSeePlayer(_player);
    }

    public bool IsPlayerHeard()
    {
        if (_player == null) return false;

        bool isCanHearPlayer = _enemyHearing.CanHearPlayer(_player);
        return isCanHearPlayer;
        // Проверяем, слышен ли игрок (например, по расстоянию)
        //float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        //_lastKnownPlayerPosition = _player.position; // УДАЛИТЬ КОГДА СДЕЛАЮ ОТДЕЛЬНЫЙ КЛАСС
        //return distanceToPlayer <= _hearingRange; // Проверяем, слышен ли игрок
    }

    public Vector3 GetPlayerPosition()
    {
        return _player != null ? _player.position : Vector3.zero; // Возвращаем позицию игрока
    }

    public Transform GetPlayerTransform()
    {
        return _player; // Возвращаем трансформ игрока
    }

    public void SetLastKnownPlayerPosition(Vector3 position)
    {
        _lastKnownPlayerPosition = position; // Сохраняем позицию последнего известного местоположения игрока
    }

    public Vector3 GetPlayerLastKnownPosition()
    {
        return _lastKnownPlayerPosition; // Возвращаем позицию последнего известного местоположения игрока
    }

    public Vector3 GetPatrolTargetPoint()
    {
        return _patrolSystem.GetNextPatrolTarget();
    }
    public void MoveTowards(Vector3 targetPosition, float speed)
    {
        _navMeshAgent.SetDestination(targetPosition);
        _navMeshAgent.speed = speed; // Устанавливаем скорость движения
    }

    public void StopMovement()
    {
        _navMeshAgent.isStopped = true; // Останавливаем NavMeshAgent
    }

    public void ResumeMovement()
    {
        _navMeshAgent.isStopped = false; // Возобновляем работу NavMeshAgent
    }
}
